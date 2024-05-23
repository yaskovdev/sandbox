#include "multiplexer.h"
#include <cstdlib>
#include <cstdio>

multiplexer::multiplexer(const char *filename, AVDictionary *opt, audio_config audio_config,
                         video_config video_config) {
    const AVCodec *audio_codec;
    const AVCodec *video_codec;
    int ret;

    /* allocate the output media context */
    avformat_alloc_output_context2(&format_context_, nullptr, nullptr, filename);
    if (!format_context_) {
        printf("Could not deduce output format from file extension: using MPEG.\n");
        avformat_alloc_output_context2(&format_context_, nullptr, "mpeg", filename);
    }
    printf("Output format is %s\n", format_context_->oformat->name);
    if (!format_context_) {
        exit(1);
    }

    const AVOutputFormat *output_format = format_context_->oformat;

    // Add the audio and video streams using the default format codecs and initialize the codecs.
    if (output_format->video_codec != AV_CODEC_ID_NONE) {
        printf("Video codec is %d\n", output_format->video_codec);
        add_stream(&video_stream_, format_context_, &video_codec, output_format->video_codec, audio_config,
                   video_config);
        has_video_ = 1;
    }
    if (output_format->audio_codec != AV_CODEC_ID_NONE) {
        add_stream(&audio_stream_, format_context_, &audio_codec, output_format->audio_codec, audio_config,
                   video_config);
        has_audio_ = 1;
    }

    /* Now that all the parameters are set, we can open the audio and
     * video codecs and allocate the necessary encode buffers. */
    if (has_video_)
        open_video(video_codec, &video_stream_, opt);

    if (has_audio_)
        open_audio(audio_codec, &audio_stream_, opt);

    av_dump_format(format_context_, 0, filename, 1);

    /* open the output file, if needed */
    if (!(output_format->flags & AVFMT_NOFILE)) {
        ret = avio_open(&format_context_->pb, filename, AVIO_FLAG_WRITE);
        if (ret < 0) {
            fprintf(stderr, "Could not open '%s': %s\n", filename, error_to_string(ret));
            exit(1);
        }
    }

    /* Write the stream header, if any. */
    ret = avformat_write_header(format_context_, &opt);
    if (ret < 0) {
        fprintf(stderr, "Error occurred when opening output file: %s\n", error_to_string(ret));
        exit(1);
    }
}

int multiplexer::write_frame(AVFormatContext *fmt_ctx, AVCodecContext *c, AVStream *st, AVFrame *frame, AVPacket *pkt) {
    int ret;

    // send the frame_ to the encoder
    ret = avcodec_send_frame(c, frame);
    if (ret < 0) {
        fprintf(stderr, "Error sending a frame_ to the encoder: %s\n", error_to_string(ret));
        exit(1);
    }

    while (ret >= 0) {
        ret = avcodec_receive_packet(c, pkt);
        if (ret == AVERROR(EAGAIN) || ret == AVERROR_EOF)
            break;
        else if (ret < 0) {
            fprintf(stderr, "Error encoding a frame_: %s\n", error_to_string(ret));
            exit(1);
        }

        /* rescale output packet timestamp values from codec to stream timebase */
        av_packet_rescale_ts(pkt, c->time_base, st->time_base);
        pkt->stream_index = st->index;

        /* Write the compressed frame_ to the media file. */
        log_packet(fmt_ctx, pkt);
        ret = av_interleaved_write_frame(fmt_ctx, pkt);
        /* pkt is now blank (av_interleaved_write_frame() takes ownership of
         * its contents and resets pkt), so that no unreferencing is necessary.
         * This would be different if one used av_write_frame(). */
        if (ret < 0) {
            fprintf(stderr, "Error while writing output packet: %s\n", error_to_string(ret));
            exit(1);
        }
    }

    return ret == AVERROR_EOF ? 1 : 0;
}

void multiplexer::log_packet(const AVFormatContext *fmt_ctx, const AVPacket *pkt) {
    AVRational *time_base = &fmt_ctx->streams[pkt->stream_index]->time_base;
    printf("pts:%s pts_time:%s dts:%s dts_time:%s duration:%s duration_time:%s stream_index:%d\n",
           ts_to_string(pkt->pts), time_to_string(pkt->pts, time_base),
           ts_to_string(pkt->dts), time_to_string(pkt->dts, time_base),
           ts_to_string(pkt->duration), time_to_string(pkt->duration, time_base),
           pkt->stream_index);
}

/* Add an output stream. */
void multiplexer::add_stream(output_stream *ost, AVFormatContext *format_context, const AVCodec **codec,
                             enum AVCodecID codec_id, audio_config audio_config,
                             video_config video_config) {
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
            ost->st->time_base = AVRational{1, c->sample_rate};
            break;

        case AVMEDIA_TYPE_VIDEO:
            c->codec_id = codec_id;

            c->bit_rate = video_config.bit_rate;
            /* Resolution must be a multiple of two. */
            c->width = video_config.width;
            c->height = video_config.height;
            /* timebase: This is the fundamental unit of time (in seconds) in terms
             * of which frame_ timestamps are represented. For fixed-fps content,
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

void multiplexer::open_audio(const AVCodec *codec, output_stream *ost, AVDictionary *opt_arg) {
    AVCodecContext *c;
    int ret;
    AVDictionary *opt = nullptr;

    c = ost->enc;

    /* open it */
    av_dict_copy(&opt, opt_arg, 0);
    ret = avcodec_open2(c, codec, &opt);
    av_dict_free(&opt);
    if (ret < 0) {
        fprintf(stderr, "Could not open audio codec: %s\n", error_to_string(ret));
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

AVFrame *multiplexer::alloc_audio_frame(enum AVSampleFormat sample_fmt, uint64_t channel_layout, int sample_rate,
                                        int nb_samples) {
    AVFrame *frame = av_frame_alloc();
    int ret;

    if (!frame) {
        fprintf(stderr, "Error allocating an audio frame_\n");
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
 * encode one audio frame_ and send it to the muxer
 * return 1 when encoding is finished, 0 otherwise
 */
int multiplexer::write_audio_frame(AVFrame *frame) {
    AVCodecContext *c;
    int ret;
    int dst_nb_samples;

    c = audio_stream_.enc;

    if (frame) {
        /* convert samples from native format to destination codec format, using the resampler */
        /* compute destination number of samples */
        dst_nb_samples = av_rescale_rnd(swr_get_delay(audio_stream_.swr_ctx, c->sample_rate) + frame->nb_samples,
                                        c->sample_rate, c->sample_rate, AV_ROUND_UP);
        av_assert0(dst_nb_samples == frame->nb_samples);

        if (audio_stream_.frame == nullptr) {
            audio_stream_.frame = alloc_audio_frame(c->sample_fmt, c->channel_layout, c->sample_rate,
                                                    frame->nb_samples);
        }

        /* when we pass a frame_ to the encoder, it may keep a reference to it
         * internally;
         * make sure we do not overwrite it here
         */
        ret = av_frame_make_writable(audio_stream_.frame);
        if (ret < 0)
            exit(1);

        /* convert to destination format */
        ret = swr_convert(audio_stream_.swr_ctx, audio_stream_.frame->data, dst_nb_samples,
                          (const uint8_t **) frame->data, frame->nb_samples);
        if (ret < 0) {
            fprintf(stderr, "Error while converting\n");
            exit(1);
        }
        frame = audio_stream_.frame;

        frame->pts = av_rescale_q(audio_stream_.samples_count, AVRational{1, c->sample_rate}, c->time_base);
        audio_stream_.samples_count += dst_nb_samples;
    }

    return write_frame(format_context_, c, audio_stream_.st, frame, audio_stream_.tmp_pkt);
}

void multiplexer::open_video(const AVCodec *codec, output_stream *ost, AVDictionary *opt_arg) {
    int ret;
    AVCodecContext *c = ost->enc;
    AVDictionary *opt = nullptr;

    av_dict_copy(&opt, opt_arg, 0);

    /* open the codec */
    ret = avcodec_open2(c, codec, &opt);
    av_dict_free(&opt);
    if (ret < 0) {
        fprintf(stderr, "Could not open video codec: %s\n", error_to_string(ret));
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
* encode one video frame_ and send it to the muxer
* return 1 when encoding is finished, 0 otherwise
*/
int multiplexer::write_video_frame(AVFrame *frame) {
    return write_frame(format_context_, video_stream_.enc, video_stream_.st, frame, video_stream_.tmp_pkt);
}

void multiplexer::finalize() {
    /* Write the trailer, if any. The trailer must be written before you
     * close the CodecContexts open when you wrote the header; otherwise
     * av_write_trailer() may try to use memory that was freed on
     * av_codec_close(). */
    av_write_trailer(format_context_);

    /* Close each codec. */
    if (has_video_)
        close_stream(&video_stream_);
    if (has_audio_)
        close_stream(&audio_stream_);

    if (!(format_context_->oformat->flags & AVFMT_NOFILE))
        /* Close the output file. */
        avio_closep(&format_context_->pb);

    /* free the stream */
    avformat_free_context(format_context_);
}

void multiplexer::close_stream(output_stream *ost) {
    avcodec_free_context(&ost->enc);
    av_frame_free(&ost->frame);
    av_packet_free(&ost->tmp_pkt);
    swr_free(&ost->swr_ctx);
}

char *multiplexer::time_to_string(int64_t ts, AVRational *tb) {
    char buf[AV_TS_MAX_STRING_SIZE];
    return ts_make_time_string(buf, ts, tb);
}

char *multiplexer::ts_make_time_string(char *buf, int64_t ts, AVRational *tb) {
    if (ts == AV_NOPTS_VALUE) snprintf(buf, AV_TS_MAX_STRING_SIZE, "NOPTS");
    else snprintf(buf, AV_TS_MAX_STRING_SIZE, "%.6g", av_q2d(*tb) * ts);
    return buf;
}

char *multiplexer::ts_to_string(int64_t ts) {
    char buf[AV_TS_MAX_STRING_SIZE];
    return av_ts_make_string(buf, ts);
}

char *multiplexer::ts_make_string(char *buf, int64_t ts) {
    if (ts == AV_NOPTS_VALUE) snprintf(buf, AV_TS_MAX_STRING_SIZE, "NOPTS");
    else snprintf(buf, AV_TS_MAX_STRING_SIZE, "%" PRId64, ts);
    return buf;
}

char *multiplexer::error_to_string(int errnum) {
    char buf[AV_TS_MAX_STRING_SIZE];
    return make_error_string(buf, AV_ERROR_MAX_STRING_SIZE, errnum);
}

char *multiplexer::make_error_string(char *errbuf, size_t errbuf_size, int errnum) {
    av_strerror(errnum, errbuf, errbuf_size);
    return errbuf;
}