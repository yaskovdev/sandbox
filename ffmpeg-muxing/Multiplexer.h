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

#define STREAM_FRAME_RATE 25 /* 25 images/s */
#define STREAM_SAMPLE_RATE 44100

typedef struct AudioConfig {
    AVRational time_base = (AVRational) {1, STREAM_SAMPLE_RATE};
    int sample_rate = STREAM_SAMPLE_RATE;
    int channels = 2;
    int channel_layout = 3;
    int nb_samples = 1024;
    int64_t bit_rate = 64000;
} AudioConfig;

typedef struct VideoConfig {
    AVRational time_base = (AVRational) {1, STREAM_FRAME_RATE};
    int width = 352;
    int height = 288;
    enum AVPixelFormat pix_fmt = AV_PIX_FMT_YUV420P;
    int64_t bit_rate = 400000;
    /* emit one intra frame every twelve frames at most */
    int gop_size = 12;
} VideoConfig;

typedef struct OutputStream {
    AVStream *st;
    AVCodecContext *enc;

    int samples_count;

    AVFrame *frame;

    AVPacket *tmp_pkt;

    struct SwrContext *swr_ctx;
} OutputStream;

class Multiplexer {
public:
    Multiplexer();

    void initialize(const char *filename, AVDictionary *opt, AudioConfig audio_config, VideoConfig video_config);

    int write_audio_frame(AVFrame *frame);

    int write_video_frame(AVFrame *frame);

    void finalize();

private:
    OutputStream audio_st = {nullptr};
    OutputStream video_st = {nullptr};
    AVFormatContext *format_context;
    int has_audio = 0;
    int has_video = 0;

    static void log_packet(const AVFormatContext *fmt_ctx, const AVPacket *pkt);

    static int write_frame(AVFormatContext *fmt_ctx, AVCodecContext *c, AVStream *st, AVFrame *frame, AVPacket *pkt);

    static void add_stream(OutputStream *ost, AVFormatContext *format_context, const AVCodec **codec, enum AVCodecID codec_id, AudioConfig audio_config,
        VideoConfig video_config);

    static void open_audio(const AVCodec *codec, OutputStream *ost, AVDictionary *opt_arg);

    static void open_video(const AVCodec *codec, OutputStream *ost, AVDictionary *opt_arg);

    static void close_stream(OutputStream *ost);

    static AVFrame *alloc_audio_frame(AVSampleFormat sample_fmt, uint64_t channel_layout, int sample_rate, int nb_samples);
};

#endif //FFMPEG_MUXING_MULTIPLEXER_H
