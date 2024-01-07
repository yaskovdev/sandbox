#include <iostream>
#include <fstream>
#include <vector>

extern "C" {
#include "libavcodec/avcodec.h"
#include "libavutil/frame.h"
#include "libavutil/imgutils.h"
#include "libswscale/swscale.h"
}

#define SCALE true

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

void LogSupportedFormats(const AVPixelFormat *supported_formats) {
    if (supported_formats == nullptr) {
        std::cout << "H.264 decoder supported pixel formats are unknown" << "\n";
    } else {
        for (const AVPixelFormat *format = supported_formats; *format != AV_PIX_FMT_NONE; format++) {
            std::cout << "H.264 decoder supported pixel format is " << *format << "\n";
        }
    }
}

AVPixelFormat GetFormatAndLogSupportedFormats(AVCodecContext *context, const AVPixelFormat *supported_formats) {
    LogSupportedFormats(supported_formats);
    const AVPixelFormat default_format = avcodec_default_get_format(context, supported_formats);
    std::cout << "H.264 decoder format selected by default is " << default_format << "\n";
    return default_format;
}

int main(int argc, char **argv) {
    if (argc < 3) {
        std::cout << "Specify full path to input packet and output frame" << "\n";
        exit(-1);
    }
    std::cout << "Decoding started..." << "\n";

    int align = 1;

    AVCodecContext *context = create_codec_context();
    context->get_format = GetFormatAndLogSupportedFormats;

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

        if (SCALE) {
            // The width and height of the decoded frames announced by the sender. They are different from the actual width and height of the decoded frames, and that is the problem.
            int announced_width = 424;
            int announced_height = 240;
            SwsContext* sws_context = sws_getContext(announced_width, announced_height, AV_PIX_FMT_YUV420P, announced_width, announced_height, AV_PIX_FMT_NV12, SWS_BILINEAR, nullptr, nullptr, nullptr);
            uint8_t* dst_data[4];
            int dst_line_size[4];
            const int image_alloc_res = av_image_alloc(dst_data, dst_line_size, announced_width, announced_height, AV_PIX_FMT_NV12, align);
            if (image_alloc_res < 0) {
                exit(image_alloc_res);
            }

            int srcSliceY = 0;
            const AVPixFmtDescriptor *descriptor = av_pix_fmt_desc_get(AV_PIX_FMT_YUV420P);
            uint8_t chrSrcVSubSample = descriptor->log2_chroma_h;
            int srcSliceH = decoded_frame->height;
            int macro_height_src = !!(descriptor->flags & AV_PIX_FMT_FLAG_BAYER) ? 2 : (1 << chrSrcVSubSample);
            if ((srcSliceY & (macro_height_src - 1)) || ((srcSliceH & (macro_height_src - 1)) && srcSliceY + srcSliceH != announced_height) || srcSliceY + srcSliceH > announced_height) {
                exit(-1);
            }

            const int scale_res = sws_scale(sws_context, decoded_frame->data, decoded_frame->linesize, 0, decoded_frame->height, dst_data, dst_line_size);
            if (scale_res < 0) {
                exit(scale_res);
            }

            int buffer_size = av_image_get_buffer_size(AV_PIX_FMT_NV12, announced_width, announced_height, align);
            std::cout << "Buffer size is " << buffer_size << "\n";
            auto *buffer = new uint8_t[buffer_size];
            int number_of_bytes_written = av_image_copy_to_buffer(buffer, buffer_size, dst_data, dst_line_size, AV_PIX_FMT_NV12, announced_width, announced_height, align);
            std::cout << "Copied to buffer " << number_of_bytes_written << " bytes" << "\n";
            write_output_as_raw_frame(argv[2], buffer, buffer_size);
            delete[] buffer;
        } else {
            int buffer_size = av_image_get_buffer_size(pixel_format, width, height, align);
            std::cout << "Buffer size is " << buffer_size << "\n";
            auto *buffer = new uint8_t[buffer_size];
            int number_of_bytes_written = av_image_copy_to_buffer(buffer, buffer_size, decoded_frame->data, line_size, pixel_format, width, height, align);
            std::cout << "Copied to buffer " << number_of_bytes_written << " bytes" << "\n";
            write_output_as_raw_frame(argv[2], buffer, buffer_size);
            delete[] buffer;
        }
    }

    av_frame_free(&decoded_frame);
    delete[] encoded_packet->data;
    av_packet_free(&encoded_packet);
    avcodec_free_context(&context);

    return receive_frame_result;
}
