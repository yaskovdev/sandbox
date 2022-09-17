#include <iostream>
#include <fstream>

extern "C" {
#include "libavcodec/avcodec.h"
#include "libavutil/frame.h"
#include "libavutil/imgutils.h"
}

static unsigned long read_input(uint8_t **input_buffer) {
    FILE *f = fopen(R"(c:\dev\tasks\2981883\212.encoded)", "rb");
    fseek(f, 0, SEEK_END);
    long size = ftell(f);
    rewind(f);
    auto buffer = (uint8_t *) malloc(sizeof(uint8_t) * size + AV_INPUT_BUFFER_PADDING_SIZE);
    size_t actual_size = fread(buffer, 1, size, f);
    *input_buffer = buffer;
    return actual_size;
}

static void write_output_as_image(unsigned char *buf, int wrap, int xsize, int ysize) {
    FILE *f = fopen(R"(c:\dev\tasks\2981883\212.png)", "w");
    fprintf(f, "P5\n%d %d\n%d\n", xsize, ysize, 255);
    for (int i = 0; i < ysize; i++) {
        fwrite(buf + i * wrap, 1, xsize, f);
    }
    fclose(f);
}

static void write_output_as_raw_frame(uint8_t *buffer, int buffer_size) {
    std::ofstream output_stream;
    output_stream.open(R"(c:\dev\tasks\2981883\212.raw)", std::ios::binary | std::ios::out);
    output_stream.write((char *) buffer, buffer_size);
    output_stream.close();
}

static AVCodecContext *create_codec_context() {
    AVCodec *codec = avcodec_find_decoder(AV_CODEC_ID_H264);
    AVCodecContext *context = avcodec_alloc_context3(codec);
    avcodec_open2(context, codec, nullptr);
    return context;
}

int main() {
    std::cout << "Decoding started..." << "\n";

    int align = 1;
    AVPacket *encoded_packet = av_packet_alloc();
    encoded_packet->size = read_input(&encoded_packet->data);

    AVCodecContext *context = create_codec_context();
    int send_packet_result = avcodec_send_packet(context, encoded_packet);
    if (send_packet_result < 0) {
        exit(send_packet_result);
    }
    AVFrame *decoded_frame = av_frame_alloc();
    int receive_frame_result = avcodec_receive_frame(context, decoded_frame);

    if (receive_frame_result == 0) {
        std::cout << decoded_frame->width << "\n";
        std::cout << decoded_frame->height << "\n";
        std::cout << decoded_frame->linesize[0] << ", " << FFALIGN(decoded_frame->linesize[0], align) << "\n";
        std::cout << decoded_frame->linesize[1] << ", " << FFALIGN(decoded_frame->linesize[1], align) << "\n";
        std::cout << decoded_frame->linesize[2] << ", " << FFALIGN(decoded_frame->linesize[2], align) << "\n";
//        write_output_as_image(decoded_frame->data[0], decoded_frame->linesize[0], context->width, context->height);
        auto pixel_format = static_cast<AVPixelFormat>(decoded_frame->format);
        int buffer_size = av_image_get_buffer_size(pixel_format, decoded_frame->width, decoded_frame->height, align);
        std::cout << "Buffer size is " << buffer_size << "\n";
        auto *buffer = new uint8_t[buffer_size];
        int number_of_bytes_written = av_image_copy_to_buffer(buffer, buffer_size, decoded_frame->data, decoded_frame->linesize, pixel_format, decoded_frame->width,decoded_frame->height, align);
        std::cout << "Copied " << number_of_bytes_written << " bytes" << "\n";
        write_output_as_raw_frame(buffer, buffer_size);
    }
    return receive_frame_result;
}
