#include "audio_frame_generator.h"

audio_frame_generator::audio_frame_generator(audio_config config, int stream_duration) {
    this->next_pts_ = 0;
    this->time_base_ = config.time_base;
    this->channels_ = config.channels;
    this->stream_duration_ = stream_duration;
    this->t_ = 0;
    this->t_incr_ = 2 * M_PI * 110.0 / config.sample_rate;
    this->t_incr2_ = 2 * M_PI * 110.0 / config.sample_rate / config.sample_rate;
    this->frame_ = alloc_audio_frame(config.channel_layout, config.sample_rate, config.nb_samples);
}

/* Prepare a 16 bit dummy audio frame_ of 'frame_size' samples and 'nb_channels' channels_. */
AVFrame *audio_frame_generator::generate_audio_frame() {
    int j, i, v;
    auto *q = (int16_t *) frame_->data[0];

    /* check if we want to generate more frames */
    if (av_compare_ts(next_pts_, time_base_, stream_duration_, AVRational{1, 1}) > 0)
        return nullptr;

    for (j = 0; j < frame_->nb_samples; j++) {
        v = (int) (sin(t_) * 10000);
        for (i = 0; i < channels_; i++)
            *q++ = v;
        t_ += t_incr_;
        t_incr_ += t_incr2_;
    }

    frame_->pts = next_pts_;
    next_pts_ += frame_->nb_samples;

    return frame_;
}

AVFrame *audio_frame_generator::alloc_audio_frame(uint64_t channel_layout, int sample_rate, int nb_samples) {
    AVFrame *frame = av_frame_alloc();

    if (!frame) {
        fprintf(stderr, "Error allocating an audio frame_\n");
        exit(1);
    }

    frame->format = AV_SAMPLE_FMT_S16;
    frame->channel_layout = channel_layout;
    frame->sample_rate = sample_rate;
    frame->nb_samples = nb_samples;

    if (nb_samples) {
        if (av_frame_get_buffer(frame, 0) < 0) {
            fprintf(stderr, "Error allocating an audio buffer\n");
            exit(1);
        }
    }

    return frame;
}
