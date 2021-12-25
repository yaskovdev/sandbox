#ifndef FFMPEG_MUXING_VIDEO_FRAME_GENERATOR_H
#define FFMPEG_MUXING_VIDEO_FRAME_GENERATOR_H

#include "multiplexer.h"

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

class video_frame_generator {
public:
    video_frame_generator(video_config config, int stream_duration);

    AVFrame *generate_video_frame();

private:
    int next_pts_;
    AVRational time_base_;
    AVFrame *frame_;
    int width_;
    int height_;
    enum AVPixelFormat pix_fmt_;
    int stream_duration_;

    static void fill_yuv_image(AVFrame *pict, int frame_index, int width, int height);

    static AVFrame *alloc_picture(AVPixelFormat pix_fmt, int width, int height);
};

#endif //FFMPEG_MUXING_VIDEO_FRAME_GENERATOR_H
