#include <cstdio>
#include <cstring>
#include "Multiplexer.h"

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
        if (!strcmp(argv[i], "-flags") || !strcmp(argv[i], "-fflags")) { av_dict_set(&opt, argv[i] + 1, argv[i + 1], 0); }
    }
    Multiplexer multiplexer;
    return multiplexer.multiplex(filename, opt);
}
