#include <iostream>
#include <fstream>
#include <vector>

extern "C" {
#include "libavcodec/avcodec.h"
#include "libavutil/frame.h"
#include "libavutil/imgutils.h"
}

static int read_input(uint8_t **input_buffer) {
    std::ifstream input(R"(c:\dev\tasks\2981883\212.encoded)", std::ios::binary);
    std::vector<uint8_t> bytes((std::istreambuf_iterator<char>(input)), std::istreambuf_iterator<char>());
    input.close();
    *input_buffer = &bytes[0];
    return static_cast<int>(bytes.size());
}

static void write_output_as_image(unsigned char *buffer, int wrap, int width, int height) {
    FILE *f = fopen(R"(c:\dev\tasks\2981883\212.png)", "w");
    fprintf(f, "P5\n%d %d\n%d\n", width, height, 255);
    for (int i = 0; i < height; i++) {
        fwrite(buffer + i * wrap, 1, width, f);
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
    const AVCodec *codec = avcodec_find_decoder(AV_CODEC_ID_H264);
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
        int width = decoded_frame->width;
        int height = decoded_frame->height;
        int *line_size = decoded_frame->linesize;
        std::cout << width << "\n";
        std::cout << height << "\n";
        std::cout << line_size[0] << ", " << FFALIGN(line_size[0], align) << "\n";
        std::cout << line_size[1] << ", " << FFALIGN(line_size[1], align) << "\n";
        std::cout << line_size[2] << ", " << FFALIGN(line_size[2], align) << "\n";
//        write_output_as_image(decoded_frame->data[0], line_size[0], context->width, context->height);
        auto pixel_format = static_cast<AVPixelFormat>(decoded_frame->format);
        int buffer_size = av_image_get_buffer_size(pixel_format, width, height, align);
        std::cout << "Buffer size is " << buffer_size << "\n";
        auto *buffer = new uint8_t[buffer_size];
        int number_of_bytes_written = av_image_copy_to_buffer(buffer, buffer_size, decoded_frame->data, line_size, pixel_format, width, height, align);
        std::cout << "Copied " << number_of_bytes_written << " bytes" << "\n";
        write_output_as_raw_frame(buffer, buffer_size);
    }
    return receive_frame_result;
}
