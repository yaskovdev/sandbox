#include "video_frame_generator.h"

video_frame_generator::video_frame_generator(video_config config, int stream_duration) {
    this->next_pts_ = 0;
    this->time_base_ = config.time_base;
    this->width_ = config.width;
    this->height_ = config.height;
    this->pix_fmt_ = config.pix_fmt;
    this->stream_duration_ = stream_duration;
    this->frame_ = alloc_picture(config.pix_fmt, config.width, config.height); // TODO: free the frame_ afterwards
    if (!this->frame_) {
        fprintf(stderr, "Could not allocate video frame_\n");
        exit(1);
    }
}

AVFrame *video_frame_generator::generate_video_frame() {
    /* check if we want to generate more frames */
    if (av_compare_ts(next_pts_, time_base_, stream_duration_, AVRational{1, 1}) > 0) { return nullptr; }

    /* when we pass a frame_ to the encoder, it may keep a reference to it
     * internally; make sure we do not overwrite it here */
    if (av_frame_make_writable(frame_) < 0) { exit(1); }

    if (pix_fmt_ == AV_PIX_FMT_YUV420P) {
        fill_yuv_image(frame_, next_pts_, width_, height_);
    } else {
        fprintf(stderr, "Unexpected pixel format %d\n", pix_fmt_);
        exit(1);
    }

    frame_->pts = next_pts_++;

    return frame_;
}

AVFrame *video_frame_generator::alloc_picture(enum AVPixelFormat pix_fmt, int width, int height) {
    AVFrame *picture;
    int ret;

    picture = av_frame_alloc();
    if (!picture)
        return nullptr;

    picture->format = pix_fmt;
    picture->width = width;
    picture->height = height;

    /* allocate the buffers for the frame_ data */
    ret = av_frame_get_buffer(picture, 0);
    if (ret < 0) {
        fprintf(stderr, "Could not allocate frame_ data.\n");
        exit(1);
    }

    return picture;
}

/* Prepare a dummy image. */
void video_frame_generator::fill_yuv_image(AVFrame *pict, int frame_index, int width, int height) {
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
