#include <cstdio>
#include <cstring>
#include "Multiplexer.h"
#include "VideoFrameGenerator.h"
#include "AudioFrameGenerator.h"

#define STREAM_DURATION   10.0

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

    multiplexer.initialize(filename, opt);

    int sample_rate = 44100;
    int channels = 2;
    int channel_layout = 3;
    int nb_samples = 1024;
    AudioFrameGenerator audio_frame_generator((AVRational) {1, sample_rate}, channels, STREAM_DURATION, channel_layout, sample_rate, nb_samples);
    AVFrame *audio_frame;
    do {
        audio_frame = audio_frame_generator.generate_audio_frame();
        multiplexer.write_audio_frame(audio_frame);
    } while (audio_frame != nullptr);

    // TODO: duplicates
    VideoFrameGenerator video_frame_generator((AVRational) {1, STREAM_FRAME_RATE}, 352, 288, AV_PIX_FMT_YUV420P, STREAM_DURATION);
    AVFrame *video_frame;
    do {
        video_frame = video_frame_generator.generate_video_frame();
        multiplexer.write_video_frame(video_frame);
    } while (video_frame != nullptr);

    multiplexer.finalize();

    return 0;
}
