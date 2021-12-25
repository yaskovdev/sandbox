#include <cstdio>
#include <cstring>
#include "multiplexer.h"
#include "video_frame_generator.h"
#include "audio_frame_generator.h"

#define STREAM_DURATION 10.0

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
    audio_config audio_config;
    video_config video_config;
    multiplexer multiplexer(filename, opt, audio_config, video_config);

    audio_frame_generator audio_frame_generator(audio_config, STREAM_DURATION);
    AVFrame *audio_frame;
    do {
        audio_frame = audio_frame_generator.generate_audio_frame();
        multiplexer.write_audio_frame(audio_frame);
    } while (audio_frame != nullptr);

    video_frame_generator video_frame_generator(video_config, STREAM_DURATION);
    AVFrame *video_frame;
    do {
        video_frame = video_frame_generator.generate_video_frame();
        multiplexer.write_video_frame(video_frame);
    } while (video_frame != nullptr);

    multiplexer.finalize();

    return 0;
}
