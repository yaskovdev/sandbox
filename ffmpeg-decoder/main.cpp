#include <iostream>
#include <fstream>
#include <vector>

extern "C" {
#include "libavcodec/avcodec.h"
#include "libavutil/frame.h"
#include "libavutil/imgutils.h"
}

static int read_input(char *file_name, uint8_t **input_buffer) {
    std::ifstream input_stream(file_name, std::ios::binary);
    std::vector<uint8_t> bytes((std::istreambuf_iterator<char>(input_stream)), std::istreambuf_iterator<char>());
    input_stream.close();
    *input_buffer = new uint8_t[bytes.size() + AV_INPUT_BUFFER_PADDING_SIZE];
    std::copy(bytes.begin(), bytes.end(), *input_buffer);
    return static_cast<int>(bytes.size());
}

static void write_output_as_raw_frame(char *file_name, uint8_t *buffer, int buffer_size) {
    std::ofstream output_stream;
    output_stream.open(file_name, std::ios::binary);
    output_stream.write((char *) buffer, buffer_size);
    output_stream.close();
}

static AVCodecContext *create_codec_context() {
    const AVCodec *codec = avcodec_find_decoder(AV_CODEC_ID_H264);
    AVCodecContext *context = avcodec_alloc_context3(codec);
    avcodec_open2(context, codec, nullptr);
    return context;
}

int main(int argc, char **argv) {
    if (argc < 3) {
        std::cout << "Specify full path to input packet and output frame" << "\n";
        exit(-1);
    }
    std::cout << "Decoding started..." << "\n";

    int align = 1;

    AVCodecContext *context = create_codec_context();
    AVPacket *encoded_packet = av_packet_alloc();
    encoded_packet->size = read_input(argv[1], &encoded_packet->data);
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
        auto pixel_format = static_cast<AVPixelFormat>(decoded_frame->format);
        std::cout << pixel_format << "\n";
        int buffer_size = av_image_get_buffer_size(pixel_format, width, height, align);
        std::cout << "Buffer size is " << buffer_size << "\n";
        auto *buffer = new uint8_t[buffer_size];
        int number_of_bytes_written = av_image_copy_to_buffer(buffer, buffer_size, decoded_frame->data, line_size, pixel_format, width, height, align);
        std::cout << "Copied to buffer " << number_of_bytes_written << " bytes" << "\n";
        write_output_as_raw_frame(argv[2], buffer, buffer_size);
        delete[] buffer;
    }

    av_frame_free(&decoded_frame);
    delete[] encoded_packet->data;
    av_packet_free(&encoded_packet);
    avcodec_free_context(&context);

    return receive_frame_result;
}
