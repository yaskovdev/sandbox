#include <iostream>

extern "C" {
#include "libavformat/avformat.h"
#include "libavcodec/avcodec.h"
}

#define INBUF_SIZE 4096
#define AUDIO_INBUF_SIZE 20480
#define AUDIO_REFILL_THRESH 4096

static void pgm_save(unsigned char *buf, int wrap, int xsize, int ysize) {
    FILE *f;
    int i;

    f = fopen("/Users/yaskovdev/637637760230407715.decoded", "w");
    fprintf(f, "P5\n%d %d\n%d\n", xsize, ysize, 255);
    for (i = 0; i < ysize; i++)
        fwrite(buf + i * wrap, 1, xsize, f);
    fclose(f);
}

int main() {
    std::cout << "Hello, World!" << std::endl;
//    avcodec_decode_video2(c, picture, &got_picture, &avpkt);
    av_register_all();
    avcodec_register_all();
    AVCodec *codec = avcodec_find_decoder(AV_CODEC_ID_H264);
    AVCodecContext *c = avcodec_alloc_context3(codec);
    avcodec_open2(c, codec, NULL);
    FILE *f = fopen("/Users/yaskovdev/637637760230407715", "rb");
    fseek(f, 0, SEEK_END);
    long size = ftell(f);
    rewind(f);
    std::cout << size << std::endl;

    uint8_t *inbuf = (uint8_t *) malloc(sizeof(uint8_t) * size + AV_INPUT_BUFFER_PADDING_SIZE);
    AVFrame *frame = av_frame_alloc();
    AVPacket packet;
    av_init_packet(&packet);
    packet.size = fread(inbuf, 1, size, f);
    packet.data = inbuf;
    int got_frame;
    avcodec_decode_video2(c, frame, &got_frame, &packet);
    if (got_frame) {
        pgm_save(frame -> data[0], frame -> linesize[0], c -> width, c -> height);
    }
    return 0;
}
