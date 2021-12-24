#include "Multiplexer.h"
#include <cstdlib>
#include <cstdio>

void Multiplexer::log_packet(const AVFormatContext *fmt_ctx, const AVPacket *pkt) {
    AVRational *time_base = &fmt_ctx->streams[pkt->stream_index]->time_base;

    printf("pts:%s pts_time:%s dts:%s dts_time:%s duration:%s duration_time:%s stream_index:%d\n",
        av_ts2str(pkt->pts), av_ts2timestr(pkt->pts, time_base),
        av_ts2str(pkt->dts), av_ts2timestr(pkt->dts, time_base),
        av_ts2str(pkt->duration), av_ts2timestr(pkt->duration, time_base),
        pkt->stream_index);
}

Multiplexer::Multiplexer(const char *filename, AVDictionary *opt, AudioConfig audio_config, VideoConfig video_config) {
    const AVCodec *audio_codec;
    const AVCodec *video_codec;
    int ret;

    /* allocate the output media context */
    avformat_alloc_output_context2(&format_context, nullptr, nullptr, filename);
    if (!format_context) {
        printf("Could not deduce output format from file extension: using MPEG.\n");
        avformat_alloc_output_context2(&format_context, nullptr, "mpeg", filename);
    }
    printf("Output format is %s\n", format_context->oformat->name);
    if (!format_context) {
        exit(1);
    }

    const AVOutputFormat *output_format = format_context->oformat;

    // Add the audio and video streams using the default format codecs and initialize the codecs.
    if (output_format->video_codec != AV_CODEC_ID_NONE) {
        printf("Video codec is %d\n", output_format->video_codec);
        add_stream(&video_stream, format_context, &video_codec, output_format->video_codec, audio_config, video_config);
        has_video = 1;
    }
    if (output_format->audio_codec != AV_CODEC_ID_NONE) {
        add_stream(&audio_stream, format_context, &audio_codec, output_format->audio_codec, audio_config, video_config);
        has_audio = 1;
    }

    /* Now that all the parameters are set, we can open the audio and
     * video codecs and allocate the necessary encode buffers. */
    if (has_video)
        open_video(video_codec, &video_stream, opt);

    if (has_audio)
        open_audio(audio_codec, &audio_stream, opt);

    av_dump_format(format_context, 0, filename, 1);

    /* open the output file, if needed */
    if (!(output_format->flags & AVFMT_NOFILE)) {
        ret = avio_open(&format_context->pb, filename, AVIO_FLAG_WRITE);
        if (ret < 0) {
            fprintf(stderr, "Could not open '%s': %s\n", filename, av_err2str(ret));
            exit(1);
        }
    }

    /* Write the stream header, if any. */
    ret = avformat_write_header(format_context, &opt);
    if (ret < 0) {
        fprintf(stderr, "Error occurred when opening output file: %s\n", av_err2str(ret));
        exit(1);
    }
}

int Multiplexer::write_frame(AVFormatContext *fmt_ctx, AVCodecContext *c, AVStream *st, AVFrame *frame, AVPacket *pkt) {
    int ret;

    // send the frame to the encoder
    ret = avcodec_send_frame(c, frame);
    if (ret < 0) {
        fprintf(stderr, "Error sending a frame to the encoder: %s\n", av_err2str(ret));
        exit(1);
    }

    while (ret >= 0) {
        ret = avcodec_receive_packet(c, pkt);
        if (ret == AVERROR(EAGAIN) || ret == AVERROR_EOF)
            break;
        else if (ret < 0) {
            fprintf(stderr, "Error encoding a frame: %s\n", av_err2str(ret));
            exit(1);
        }

        /* rescale output packet timestamp values from codec to stream timebase */
        av_packet_rescale_ts(pkt, c->time_base, st->time_base);
        pkt->stream_index = st->index;

        /* Write the compressed frame to the media file. */
        log_packet(fmt_ctx, pkt);
        ret = av_interleaved_write_frame(fmt_ctx, pkt);
        /* pkt is now blank (av_interleaved_write_frame() takes ownership of
         * its contents and resets pkt), so that no unreferencing is necessary.
         * This would be different if one used av_write_frame(). */
        if (ret < 0) {
            fprintf(stderr, "Error while writing output packet: %s\n", av_err2str(ret));
            exit(1);
        }
    }

    return ret == AVERROR_EOF ? 1 : 0;
}

/* Add an output stream. */
void Multiplexer::add_stream(OutputStream *ost, AVFormatContext *format_context, const AVCodec **codec, enum AVCodecID codec_id, AudioConfig audio_config,
    VideoConfig video_config) {
    AVCodecContext *c;
    int i;

    /* find the encoder */
    *codec = avcodec_find_encoder(codec_id);
    if (!(*codec)) {
        fprintf(stderr, "Could not find encoder for '%s'\n", avcodec_get_name(codec_id));
        exit(1);
    }

    ost->tmp_pkt = av_packet_alloc();
    if (!ost->tmp_pkt) {
        fprintf(stderr, "Could not allocate AVPacket\n");
        exit(1);
    }

    ost->st = avformat_new_stream(format_context, nullptr);
    if (!ost->st) {
        fprintf(stderr, "Could not allocate stream\n");
        exit(1);
    }
    ost->st->id = format_context->nb_streams - 1;
    c = avcodec_alloc_context3(*codec);
    if (!c) {
        fprintf(stderr, "Could not alloc an encoding context\n");
        exit(1);
    }
    ost->enc = c;

    switch ((*codec)->type) {
        case AVMEDIA_TYPE_AUDIO:
            c->sample_fmt = (*codec)->sample_fmts ? (*codec)->sample_fmts[0] : AV_SAMPLE_FMT_FLTP;
            c->bit_rate = audio_config.bit_rate;
            c->sample_rate = audio_config.sample_rate;
            if ((*codec)->supported_samplerates) {
                c->sample_rate = (*codec)->supported_samplerates[0];
                for (i = 0; (*codec)->supported_samplerates[i]; i++) {
                    if ((*codec)->supported_samplerates[i] == audio_config.sample_rate) {
                        c->sample_rate = audio_config.sample_rate;
                    }
                }
            }
            c->channels = av_get_channel_layout_nb_channels(c->channel_layout);
            c->channel_layout = AV_CH_LAYOUT_STEREO;
            if ((*codec)->channel_layouts) {
                c->channel_layout = (*codec)->channel_layouts[0];
                for (i = 0; (*codec)->channel_layouts[i]; i++) {
                    if ((*codec)->channel_layouts[i] == AV_CH_LAYOUT_STEREO) {
                        c->channel_layout = AV_CH_LAYOUT_STEREO;
                    }
                }
            }
            c->channels = av_get_channel_layout_nb_channels(c->channel_layout);
            ost->st->time_base = (AVRational) {1, c->sample_rate};
            break;

        case AVMEDIA_TYPE_VIDEO:
            c->codec_id = codec_id;

            c->bit_rate = video_config.bit_rate;
            /* Resolution must be a multiple of two. */
            c->width = video_config.width;
            c->height = video_config.height;
            /* timebase: This is the fundamental unit of time (in seconds) in terms
             * of which frame timestamps are represented. For fixed-fps content,
             * timebase should be 1/framerate and timestamp increments should be
             * identical to 1. */
            ost->st->time_base = video_config.time_base;
            c->time_base = ost->st->time_base;

            c->gop_size = video_config.gop_size;
            c->pix_fmt = video_config.pix_fmt;
            break;

        default:
            break;
    }

    /* Some formats want stream headers to be separate. */
    if (format_context->oformat->flags & AVFMT_GLOBALHEADER)
        c->flags |= AV_CODEC_FLAG_GLOBAL_HEADER;
}

void Multiplexer::open_audio(const AVCodec *codec, OutputStream *ost, AVDictionary *opt_arg) {
    AVCodecContext *c;
    int ret;
    AVDictionary *opt = nullptr;

    c = ost->enc;

    /* open it */
    av_dict_copy(&opt, opt_arg, 0);
    ret = avcodec_open2(c, codec, &opt);
    av_dict_free(&opt);
    if (ret < 0) {
        fprintf(stderr, "Could not open audio codec: %s\n", av_err2str(ret));
        exit(1);
    }

    /* copy the stream parameters to the muxer */
    ret = avcodec_parameters_from_context(ost->st->codecpar, c);
    if (ret < 0) {
        fprintf(stderr, "Could not copy the stream parameters\n");
        exit(1);
    }

    /* create resampler context */
    ost->swr_ctx = swr_alloc();
    if (!ost->swr_ctx) {
        fprintf(stderr, "Could not allocate resampler context\n");
        exit(1);
    }

    /* set options */
    av_opt_set_int(ost->swr_ctx, "in_channel_count", c->channels, 0);
    av_opt_set_int(ost->swr_ctx, "in_sample_rate", c->sample_rate, 0);
    av_opt_set_sample_fmt(ost->swr_ctx, "in_sample_fmt", AV_SAMPLE_FMT_S16, 0);
    av_opt_set_int(ost->swr_ctx, "out_channel_count", c->channels, 0);
    av_opt_set_int(ost->swr_ctx, "out_sample_rate", c->sample_rate, 0);
    av_opt_set_sample_fmt(ost->swr_ctx, "out_sample_fmt", c->sample_fmt, 0);

    /* initialize the resampling context */
    if (swr_init(ost->swr_ctx) < 0) {
        fprintf(stderr, "Failed to initialize the resampling context\n");
        exit(1);
    }
}

AVFrame *Multiplexer::alloc_audio_frame(enum AVSampleFormat sample_fmt, uint64_t channel_layout, int sample_rate, int nb_samples) {
    AVFrame *frame = av_frame_alloc();
    int ret;

    if (!frame) {
        fprintf(stderr, "Error allocating an audio frame\n");
        exit(1);
    }

    frame->format = sample_fmt;
    frame->channel_layout = channel_layout;
    frame->sample_rate = sample_rate;
    frame->nb_samples = nb_samples;

    if (nb_samples) {
        ret = av_frame_get_buffer(frame, 0);
        if (ret < 0) {
            fprintf(stderr, "Error allocating an audio buffer\n");
            exit(1);
        }
    }

    return frame;
}

/*
 * encode one audio frame and send it to the muxer
 * return 1 when encoding is finished, 0 otherwise
 */
int Multiplexer::write_audio_frame(AVFrame *frame) {
    AVCodecContext *c;
    int ret;
    int dst_nb_samples;

    c = audio_stream.enc;

    if (frame) {
        /* convert samples from native format to destination codec format, using the resampler */
        /* compute destination number of samples */
        dst_nb_samples = av_rescale_rnd(swr_get_delay(audio_stream.swr_ctx, c->sample_rate) + frame->nb_samples, c->sample_rate, c->sample_rate, AV_ROUND_UP);
        av_assert0(dst_nb_samples == frame->nb_samples);

        if (audio_stream.frame == nullptr) {
            audio_stream.frame = alloc_audio_frame(c->sample_fmt, c->channel_layout, c->sample_rate, frame->nb_samples);
        }

        /* when we pass a frame to the encoder, it may keep a reference to it
         * internally;
         * make sure we do not overwrite it here
         */
        ret = av_frame_make_writable(audio_stream.frame);
        if (ret < 0)
            exit(1);

        /* convert to destination format */
        ret = swr_convert(audio_stream.swr_ctx, audio_stream.frame->data, dst_nb_samples, (const uint8_t **) frame->data, frame->nb_samples);
        if (ret < 0) {
            fprintf(stderr, "Error while converting\n");
            exit(1);
        }
        frame = audio_stream.frame;

        frame->pts = av_rescale_q(audio_stream.samples_count, (AVRational) {1, c->sample_rate}, c->time_base);
        audio_stream.samples_count += dst_nb_samples;
    }

    return write_frame(format_context, c, audio_stream.st, frame, audio_stream.tmp_pkt);
}

void Multiplexer::open_video(const AVCodec *codec, OutputStream *ost, AVDictionary *opt_arg) {
    int ret;
    AVCodecContext *c = ost->enc;
    AVDictionary *opt = nullptr;

    av_dict_copy(&opt, opt_arg, 0);

    /* open the codec */
    ret = avcodec_open2(c, codec, &opt);
    av_dict_free(&opt);
    if (ret < 0) {
        fprintf(stderr, "Could not open video codec: %s\n", av_err2str(ret));
        exit(1);
    }

    /* copy the stream parameters to the muxer */
    ret = avcodec_parameters_from_context(ost->st->codecpar, c);
    if (ret < 0) {
        fprintf(stderr, "Could not copy the stream parameters\n");
        exit(1);
    }
}

/*
* encode one video frame and send it to the muxer
* return 1 when encoding is finished, 0 otherwise
*/
int Multiplexer::write_video_frame(AVFrame *frame) {
    return write_frame(format_context, video_stream.enc, video_stream.st, frame, video_stream.tmp_pkt);
}

void Multiplexer::finalize() {
    /* Write the trailer, if any. The trailer must be written before you
     * close the CodecContexts open when you wrote the header; otherwise
     * av_write_trailer() may try to use memory that was freed on
     * av_codec_close(). */
    av_write_trailer(format_context);

    /* Close each codec. */
    if (has_video)
        close_stream(&video_stream);
    if (has_audio)
        close_stream(&audio_stream);

    if (!(format_context->oformat->flags & AVFMT_NOFILE))
        /* Close the output file. */
        avio_closep(&format_context->pb);

    /* free the stream */
    avformat_free_context(format_context);
}

void Multiplexer::close_stream(OutputStream *ost) {
    avcodec_free_context(&ost->enc);
    av_frame_free(&ost->frame);
    av_packet_free(&ost->tmp_pkt);
    swr_free(&ost->swr_ctx);
}
