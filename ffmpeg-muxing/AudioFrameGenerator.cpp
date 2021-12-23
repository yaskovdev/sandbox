#include "AudioFrameGenerator.h"

AudioFrameGenerator::AudioFrameGenerator(AVRational time_base, int channels, int stream_duration, uint64_t channel_layout, int sample_rate, int nb_samples) {
    this->next_pts = 0;
    this->time_base = time_base;
    this->channels = channels;
    this->stream_duration = stream_duration;
    this->t = 0;
    this->tincr = 2 * M_PI * 110.0 / sample_rate;
    this->tincr2 = 2 * M_PI * 110.0 / sample_rate / sample_rate;
    this->frame = alloc_audio_frame(channel_layout, sample_rate, nb_samples);
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
    int ret;

    if (!frame) {
        fprintf(stderr, "Error allocating an audio frame\n");
        exit(1);
    }

    frame->format = 1;
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
