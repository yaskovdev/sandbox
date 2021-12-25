#ifndef FFMPEG_MUXING_AUDIO_FRAME_GENERATOR_H
#define FFMPEG_MUXING_AUDIO_FRAME_GENERATOR_H

#include "multiplexer.h"

extern "C" {
#include <libavutil/frame.h>
}

class audio_frame_generator {
public:
    audio_frame_generator(audio_config config, int stream_duration);

    AVFrame *generate_audio_frame();

private:
    int next_pts_;
    AVRational time_base_;
    int channels_;
    int stream_duration_;
    AVFrame *frame_;
    float t_;
    float t_incr_;
    float t_incr2_;

    static AVFrame *alloc_audio_frame(uint64_t channel_layout, int sample_rate, int nb_samples);
};


#endif //FFMPEG_MUXING_AUDIO_FRAME_GENERATOR_H
