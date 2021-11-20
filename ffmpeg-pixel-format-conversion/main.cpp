#include <iostream>

extern "C" {
#include "libavformat/avformat.h"
#include "libavcodec/avcodec.h"
#include "libswscale/swscale.h"
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
    AVCodec *codec = avcodec_find_decoder(AV_CODEC_ID_H264);
    AVCodecContext *context = avcodec_alloc_context3(codec);
    avcodec_open2(context, codec, nullptr);
    return context;
}

int main() {
    std::cout << "Decoding started..." << std::endl;

    AVPacket encoded_frame;
    av_init_packet(&encoded_frame);
    encoded_frame.size = read_input(&encoded_frame.data);

    AVCodecContext *context = create_codec_context();
    int send_packet_result = avcodec_send_packet(context, &encoded_frame);
    if (send_packet_result < 0) {
        exit(send_packet_result);
    }
    AVFrame *decoded_frame = av_frame_alloc();
    int receive_frame_result = avcodec_receive_frame(context, decoded_frame);

    if (receive_frame_result == 0) {
        auto src_format = static_cast<AVPixelFormat>(decoded_frame->format);
        auto dst_format = AV_PIX_FMT_YUVJ420P;
        int width = context->width;
        int height = context->height;
        SwsContext *sws_context = sws_getContext(width, height, src_format, width, height, dst_format, SWS_BICUBIC, nullptr, nullptr, nullptr);
        AVFrame *scaled_frame = av_frame_alloc();
        scaled_frame->width = width;
        scaled_frame->height = height;
        scaled_frame->format = dst_format;
        std::cout << "Allocated buffers with " << av_frame_get_buffer(scaled_frame, 0) << std::endl;
        sws_scale(sws_context, decoded_frame->data, decoded_frame->linesize, 0, height, scaled_frame->data, scaled_frame->linesize);
        std::cout << "Converted to pixel format " << scaled_frame->format << std::endl;
    }
    return receive_frame_result;
}
