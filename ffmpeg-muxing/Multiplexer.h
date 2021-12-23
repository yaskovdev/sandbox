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

typedef struct OutputStream {
    AVStream *st;
    AVCodecContext *enc;

    /* pts of the next frame that will be generated */
    int64_t next_pts;
    int samples_count;

    AVFrame *frame;
    AVFrame *tmp_frame;

    AVPacket *tmp_pkt;

    float t, tincr, tincr2;

    struct SwsContext *sws_ctx;
    struct SwrContext *swr_ctx;
} OutputStream;

class Multiplexer {
public:
    Multiplexer();

    int multiplex(const char *filename, AVDictionary *opt);

    int write_audio_frame(AVFormatContext *oc, OutputStream *ost, AVFrame *frame);

    int write_video_frame(AVFormatContext *oc, OutputStream *ost, AVFrame *frame);

private:
    static void log_packet(const AVFormatContext *fmt_ctx, const AVPacket *pkt);

    static int write_frame(AVFormatContext *fmt_ctx, AVCodecContext *c, AVStream *st, AVFrame *frame, AVPacket *pkt);

    static void add_stream(OutputStream *ost, AVFormatContext *oc, const AVCodec **codec, enum AVCodecID codec_id);

    static AVFrame *alloc_audio_frame(enum AVSampleFormat sample_fmt, uint64_t channel_layout, int sample_rate, int nb_samples);

    static void open_audio(const AVCodec *codec, OutputStream *ost, AVDictionary *opt_arg);

    static AVFrame *get_audio_frame(OutputStream *ost);

    static AVFrame *alloc_picture(enum AVPixelFormat pix_fmt, int width, int height);

    static void open_video(const AVCodec *codec, OutputStream *ost, AVDictionary *opt_arg);

    static void fill_yuv_image(AVFrame *pict, int frame_index, int width, int height);

    static AVFrame *get_video_frame(OutputStream *ost);

    static void close_stream(AVFormatContext *oc, OutputStream *ost);
};

#endif //FFMPEG_MUXING_MULTIPLEXER_H
