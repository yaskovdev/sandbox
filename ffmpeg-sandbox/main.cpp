#include <iostream>

extern "C" {
#include "libavformat/avformat.h"
#include "libavcodec/avcodec.h"
}

static unsigned long read_input(uint8_t **input_buffer) {
    FILE *f = fopen("/Users/yaskovdev/637637760230407715", "rb");
    fseek(f, 0, SEEK_END);
    long size = ftell(f);
    rewind(f);
    auto buffer = (uint8_t *) malloc(sizeof(uint8_t) * size + AV_INPUT_BUFFER_PADDING_SIZE);
    size_t actual_size = fread(buffer, 1, size, f);
    *input_buffer = buffer;
    return actual_size;
}

static void write_output(unsigned char *buf, int wrap, int xsize, int ysize) {
    FILE *f = fopen("/Users/yaskovdev/637637760230407715.decoded.png", "w");
    fprintf(f, "P5\n%d %d\n%d\n", xsize, ysize, 255);
    for (int i = 0; i < ysize; i++) {
        fwrite(buf + i * wrap, 1, xsize, f);
    }
    fclose(f);
}

static AVCodecContext *create_codec_context() {
    av_register_all();
    avcodec_register_all();
    AVCodec *codec = avcodec_find_decoder(AV_CODEC_ID_H264);
    AVCodecContext *c = avcodec_alloc_context3(codec);
    avcodec_open2(c, codec, nullptr);
    return c;
}

int main() {
    std::cout << "Decoding started..." << std::endl;

    AVPacket encoded_frame;
    av_init_packet(&encoded_frame);
    encoded_frame.size = read_input(&encoded_frame.data);

    AVCodecContext *context = create_codec_context();
    AVFrame *decoded_frame = av_frame_alloc();
    int got_frame;
    avcodec_decode_video2(context, decoded_frame, &got_frame, &encoded_frame);

    std::cout << decoded_frame->width << std::endl;
    std::cout << decoded_frame->height << std::endl;
    std::cout << decoded_frame->linesize[0] << std::endl;
    std::cout << decoded_frame->linesize[1] << std::endl;
    std::cout << decoded_frame->linesize[2] << std::endl;

    if (got_frame) {
        write_output(decoded_frame->data[0], decoded_frame->linesize[0], context->width, context->height);
    }
    return 0;
}
