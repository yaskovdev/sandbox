#ifndef FFMPEG_MUXING_AUDIOFRAMEGENERATOR_H
#define FFMPEG_MUXING_AUDIOFRAMEGENERATOR_H

#include "Multiplexer.h"

extern "C" {
#include <libavutil/frame.h>
}

class AudioFrameGenerator {
public:
    AudioFrameGenerator(AudioConfig config, int stream_duration);

    AVFrame *generate_audio_frame();

private:
    int next_pts;
    AVRational time_base;
    int channels;
    int stream_duration;
    AVFrame *frame;
    float t;
    float tincr;
    float tincr2;

    static AVFrame *alloc_audio_frame(uint64_t channel_layout, int sample_rate, int nb_samples);
};


#endif //FFMPEG_MUXING_AUDIOFRAMEGENERATOR_H
