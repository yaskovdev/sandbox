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

AVPixelFormat concrete_get_format(struct AVCodecContext *s, const enum AVPixelFormat *fmt) {
    if (fmt == nullptr) {
        std::cout << "Callback supported decoder formats are unknown" << std::endl;
    } else {
        std::cout << "Callback supported decoder formats are:" << std::endl;
        for (int i = 0; fmt[i] != -1; i++) {
            std::cout << fmt[i] << std::endl;
        }
    }
    return AV_PIX_FMT_YUV420P;
}

static AVCodecContext *create_decoder_context() {
    AVCodec *decoder = avcodec_find_decoder(AV_CODEC_ID_H264);
    AVCodecContext *context = avcodec_alloc_context3(decoder);
    avcodec_open2(context, decoder, nullptr);
    if (decoder->pix_fmts == nullptr) {
        std::cout << "Supported decoder formats are unknown" << std::endl;
    } else {
        std::cout << "Supported decoder formats are:" << std::endl;
        for (int i = 0; decoder->pix_fmts[i] != -1; i++) {
            std::cout << decoder->pix_fmts[i] << std::endl;
        }
    }
    context->get_format = concrete_get_format;
    return context;
}

static AVCodecContext *create_encoder_context() {
    AVCodec *encoder = avcodec_find_encoder(AV_CODEC_ID_H264);
    AVCodecContext *context = avcodec_alloc_context3(encoder);
    context->width = 1620;
    context->height = 1080;
    context->pix_fmt = AV_PIX_FMT_YUVJ420P;
    context->time_base = (AVRational) {1, 25};
    avcodec_open2(context, encoder, nullptr);
    return context;
}

static void encode(AVCodecContext *enc_ctx, AVFrame *frame) {
    if (avcodec_send_frame(enc_ctx, frame) == 0) {
        std::cout << "Sent frame for encoding" << std::endl;
    } else {
        fprintf(stderr, "error sending a frame for encoding\n");
        exit(1);
    }

    AVPacket *packet = av_packet_alloc();
    int receive_packet_result = avcodec_receive_packet(enc_ctx, packet);
    while (receive_packet_result == 0) {
        printf("encoded frame %3" PRId64 " (result=%d, size=%5d)\n", packet->pts, receive_packet_result, packet->size);
        av_packet_unref(packet);
        packet = av_packet_alloc();
        receive_packet_result = avcodec_receive_packet(enc_ctx, packet);
    }

    if (receive_packet_result == AVERROR(EAGAIN)) {
        std::cout << "Waiting for more frames" << std::endl;
    } else if (receive_packet_result == AVERROR_EOF) {
        std::cout << "Encoding is finished" << std::endl;
    } else if (receive_packet_result < 0) {
        fprintf(stderr, "error during encoding\n");
        exit(1);
    }
}

int main() {
    std::cout << "Decoding started..." << std::endl;

    AVPacket encoded_frame;
    av_init_packet(&encoded_frame);
    encoded_frame.size = read_input(&encoded_frame.data);

    AVCodecContext *context = create_decoder_context();
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
        AVCodecContext *encoder = create_encoder_context();
        encode(encoder, scaled_frame);
        encode(encoder, nullptr);
    }
    return receive_frame_result;
}
