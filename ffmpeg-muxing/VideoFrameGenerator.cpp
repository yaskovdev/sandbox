//
// Created by Sergey Yaskov on 23.12.2021.
//

#include "VideoFrameGenerator.h"

VideoFrameGenerator::VideoFrameGenerator(AVRational time_base, int width, int height, enum AVPixelFormat pix_fmt, int stream_duration) {
    this->next_pts = 0;
    this->time_base = time_base;
    this->width = width;
    this->height = height;
    this->pix_fmt = pix_fmt;
    this->stream_duration = stream_duration;
    this->frame = alloc_picture(pix_fmt, width, height);
    if (!this->frame) {
        fprintf(stderr, "Could not allocate video frame\n");
        exit(1);
    }
}

AVFrame *VideoFrameGenerator::generate_video_frame() {
    /* check if we want to generate more frames */
    if (av_compare_ts(next_pts, time_base, stream_duration, (AVRational) {1, 1}) > 0) { return nullptr; }

    /* when we pass a frame to the encoder, it may keep a reference to it
     * internally; make sure we do not overwrite it here */
    if (av_frame_make_writable(frame) < 0) { exit(1); }

    if (pix_fmt == AV_PIX_FMT_YUV420P) {
        fill_yuv_image(frame, next_pts, width, height);
    } else {
        fprintf(stderr, "Unexpected pixel format %d\n", pix_fmt);
        exit(1);
    }

    frame->pts = next_pts++;

    return frame;
}

AVFrame *VideoFrameGenerator::alloc_picture(enum AVPixelFormat pix_fmt, int width, int height) {
    AVFrame *picture;
    int ret;

    picture = av_frame_alloc();
    if (!picture)
        return nullptr;

    picture->format = pix_fmt;
    picture->width = width;
    picture->height = height;

    /* allocate the buffers for the frame data */
    ret = av_frame_get_buffer(picture, 0);
    if (ret < 0) {
        fprintf(stderr, "Could not allocate frame data.\n");
        exit(1);
    }

    return picture;
}

/* Prepare a dummy image. */
void VideoFrameGenerator::fill_yuv_image(AVFrame *pict, int frame_index, int width, int height) {
    int x, y, i;

    i = frame_index;

    /* Y */
    for (y = 0; y < height; y++)
        for (x = 0; x < width; x++)
            pict->data[0][y * pict->linesize[0] + x] = x + y + i * 3;

    /* Cb and Cr */
    for (y = 0; y < height / 2; y++) {
        for (x = 0; x < width / 2; x++) {
            pict->data[1][y * pict->linesize[1] + x] = 128 + y + i * 2;
            pict->data[2][y * pict->linesize[2] + x] = 64 + x + i * 5;
        }
    }
}
