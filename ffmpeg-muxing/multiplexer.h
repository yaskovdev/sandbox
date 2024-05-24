#ifndef FFMPEG_MUXING_MULTIPLEXER_H
#define FFMPEG_MUXING_MULTIPLEXER_H

extern "C" {
#include <libavutil/avassert.h>
#include <libavutil/channel_layout.h>
#include <libavutil/opt.h>
#include <libavutil/mathematics.h>
#include <libavutil/timestamp.h>
#include <libavcodec/avcodec.h>
#include <libavformat/avformat.h>
#include <libswscale/swscale.h>
#include <libswresample/swresample.h>
}

#define AUDIO_STREAM_SAMPLE_RATE 44100
#define VIDEO_STREAM_FRAME_RATE 25

typedef struct audio_config {
    AVRational time_base = AVRational{1, AUDIO_STREAM_SAMPLE_RATE};
    int sample_rate = AUDIO_STREAM_SAMPLE_RATE;
    int channels = 2;
    int channel_layout = 3;
    int nb_samples = 1024;
    int64_t bit_rate = 64000;
} audio_config;

typedef struct video_config {
    AVRational time_base = AVRational{1, VIDEO_STREAM_FRAME_RATE};
    int width = 352;
    int height = 288;
    enum AVPixelFormat pix_fmt = AV_PIX_FMT_YUV420P;
    int64_t bit_rate = 400000;
    /* emit one intra frame_ every twelve frames at most */
    int gop_size = 12;
} video_config;

typedef struct output_stream {
    AVStream *st;
    AVCodecContext *enc;

    int samples_count;

    AVFrame *frame;

    AVPacket *tmp_pkt;

    struct SwrContext *swr_ctx;
} OutputStream;

class multiplexer {
public:
    multiplexer(const char *filename, AVDictionary *opt, audio_config audio_config, video_config video_config);

    int write_audio_frame(AVFrame *frame);

    int write_video_frame(AVFrame *frame);

    void finalize();

private:
    output_stream audio_stream_ = {};
    output_stream video_stream_ = {};
    AVFormatContext *format_context_;
    int has_audio_ = 0;
    int has_video_ = 0;

    void log_packet(const AVFormatContext *fmt_ctx, const AVPacket *pkt);

    int write_frame(AVFormatContext *fmt_ctx, AVCodecContext *c, AVStream *st, AVFrame *frame, AVPacket *pkt);

    static void add_stream(output_stream *ost, AVFormatContext *format_context, const AVCodec **codec, enum AVCodecID codec_id, audio_config audio_config,
        video_config video_config);

    void open_audio(const AVCodec *codec, output_stream *ost, AVDictionary *opt_arg);

    void open_video(const AVCodec *codec, output_stream *ost, AVDictionary *opt_arg);

    static void close_stream(output_stream *ost);

    static AVFrame *alloc_audio_frame(AVSampleFormat sample_fmt, uint64_t channel_layout, int sample_rate, int nb_samples);
};

#endif //FFMPEG_MUXING_MULTIPLEXER_H
