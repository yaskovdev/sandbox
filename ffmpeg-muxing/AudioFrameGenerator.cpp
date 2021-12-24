#include "AudioFrameGenerator.h"

AudioFrameGenerator::AudioFrameGenerator(AudioConfig config, int stream_duration) {
    this->next_pts = 0;
    this->time_base = config.time_base;
    this->channels = config.channels;
    this->stream_duration = stream_duration;
    this->t = 0;
    this->tincr = 2 * M_PI * 110.0 / config.sample_rate;
    this->tincr2 = 2 * M_PI * 110.0 / config.sample_rate / config.sample_rate;
    this->frame = alloc_audio_frame(config.channel_layout, config.sample_rate, config.nb_samples);
}

/* Prepare a 16 bit dummy audio frame of 'frame_size' samples and 'nb_channels' channels. */
AVFrame *AudioFrameGenerator::generate_audio_frame() {
    int j, i, v;
    auto *q = (int16_t *) frame->data[0];

    /* check if we want to generate more frames */
    if (av_compare_ts(next_pts, time_base, stream_duration, (AVRational) {1, 1}) > 0)
        return nullptr;

    for (j = 0; j < frame->nb_samples; j++) {
        v = (int) (sin(t) * 10000);
        for (i = 0; i < channels; i++)
            *q++ = v;
        t += tincr;
        tincr += tincr2;
    }

    frame->pts = next_pts;
    next_pts += frame->nb_samples;

    return frame;
}

AVFrame *AudioFrameGenerator::alloc_audio_frame(uint64_t channel_layout, int sample_rate, int nb_samples) {
    AVFrame *frame = av_frame_alloc();

    if (!frame) {
        fprintf(stderr, "Error allocating an audio frame\n");
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
