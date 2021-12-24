#ifndef FFMPEG_MUXING_VIDEOFRAMEGENERATOR_H
#define FFMPEG_MUXING_VIDEOFRAMEGENERATOR_H

#include "Multiplexer.h"

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

class VideoFrameGenerator {
public:
    VideoFrameGenerator(VideoConfig config, int stream_duration);

    AVFrame *generate_video_frame();

private:
    int next_pts;
    AVRational time_base;
    AVFrame *frame;
    int width;
    int height;
    enum AVPixelFormat pix_fmt;
    int stream_duration;

    static void fill_yuv_image(AVFrame *pict, int frame_index, int width, int height);

    static AVFrame *alloc_picture(AVPixelFormat pix_fmt, int width, int height);
};

#endif //FFMPEG_MUXING_VIDEOFRAMEGENERATOR_H
