#include <cstdio>
#include <cstring>
#include "Multiplexer.h"
#include "VideoFrameGenerator.h"
#include "AudioFrameGenerator.h"

#define STREAM_DURATION 10.0

AudioFrameGenerator create_audio_frame_generator(AudioConfig config) {
    AudioFrameGenerator audio_frame_generator(config.time_base, config.channels, STREAM_DURATION, config.channel_layout, config.sample_rate, config.nb_samples);
    return audio_frame_generator;
}

VideoFrameGenerator create_video_frame_generator(VideoConfig config) {
    VideoFrameGenerator video_frame_generator(config.time_base, config.width, config.height, config.pix_fmt, STREAM_DURATION);
    return video_frame_generator;
}

int main(int argc, char **argv) {
    if (argc < 2) {
        printf("usage: %s output_file\n"
               "API example program to output a media file with libavformat.\n"
               "This program generates a synthetic audio and video stream, encodes and\n"
               "muxes them into a file named output_file.\n"
               "The output format is automatically guessed according to the file extension.\n"
               "Raw images can also be output by using '%%d' in the filename.\n"
               "\n", argv[0]);
        return 1;
    }

    const char *filename = argv[1];
    AVDictionary *opt = nullptr;
    for (int i = 2; i + 1 < argc; i += 2) {
        if (!strcmp(argv[i], "-flags") || !strcmp(argv[i], "-fflags")) {
            av_dict_set(&opt, argv[i] + 1, argv[i + 1], 0);
        }
    }
    Multiplexer multiplexer;

    AudioConfig audio_config;
    VideoConfig video_config;
    multiplexer.initialize(filename, opt, audio_config, video_config);

    AudioFrameGenerator audio_frame_generator = create_audio_frame_generator(audio_config);
    AVFrame *audio_frame;
    do {
        audio_frame = audio_frame_generator.generate_audio_frame();
        multiplexer.write_audio_frame(audio_frame);
    } while (audio_frame != nullptr);

    VideoFrameGenerator video_frame_generator = create_video_frame_generator(video_config);
    AVFrame *video_frame;
    do {
        video_frame = video_frame_generator.generate_video_frame();
        multiplexer.write_video_frame(video_frame);
    } while (video_frame != nullptr);

    multiplexer.finalize();

    return 0;
}
