#include <fstream>
#include <vector>
#include <iostream>

extern "C" {
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include <libavcodec/avcodec.h>

#include <libavutil/opt.h>
#include <libavutil/imgutils.h>
}

#define FPS 16

static void encode(AVCodecContext *enc_ctx, AVFrame *frame, FILE *outfile) {
    int ret;

    /* send the frame to the encoder */
    if (frame)
        printf("Send frame %llu\n", frame->pts);

    ret = avcodec_send_frame(enc_ctx, frame);
    if (ret < 0) {
        fprintf(stderr, "Error sending a frame for encoding\n");
        exit(1);
    }

    while (true) {
        auto *pkt = new AVPacket();
        memset(pkt, 0, sizeof(AVPacket));
        ret = avcodec_receive_packet(enc_ctx, pkt);
        if (ret == AVERROR(EAGAIN) || ret == AVERROR_EOF)
            return;
        else if (ret < 0) {
            fprintf(stderr, "Error during encoding\n");
            exit(1);
        }

        printf("Write packet %llu (size=%5d)\n", pkt->pts, pkt->size);
        fwrite(pkt->data, 1, pkt->size, outfile);
        av_packet_unref(pkt);
    }
}

inline std::vector<uint8_t> read_all_bytes(const std::string &file_path) {
    std::ifstream input_stream(file_path, std::ios::in | std::ios::binary);
    std::vector<uint8_t> data((std::istreambuf_iterator<char>(input_stream)), std::istreambuf_iterator<char>());
    return data;
}

int main(int argc, char **argv) {
//    av_log_set_level(56);
    const char *filename, *codec_name;
    const AVCodec *codec;
    AVCodecContext *c = NULL;
    int ret;
    FILE *f;
    uint8_t endcode[] = {0, 0, 1, 0xb7};

    if (argc <= 1) {
        fprintf(stderr, "Usage: %s <output file> <codec name>\n", argv[0]);
        exit(0);
    }
    filename = argv[1];

    codec = avcodec_find_encoder(AV_CODEC_ID_H264);
    if (!codec) {
        fprintf(stderr, "Codec '%s' not found\n", codec_name);
        exit(1);
    }

    c = avcodec_alloc_context3(codec);
    if (!c) {
        fprintf(stderr, "Could not allocate video codec context\n");
        exit(1);
    }

    AVPacket *pkt = av_packet_alloc();
    if (!pkt)
        exit(1);

    /* put sample parameters */
    c->bit_rate = 1000000;
    /* resolution must be a multiple of two */
    c->width = 1920;
    c->height = 1080;
    /* frames per second */
    c->time_base = av_make_q(1, FPS * 1000); // NOTE: setting c->time_base and frame->pts like this and not setting c->framerate reproduce the first blurry frames!
//    c->framerate = av_make_q(FPS, 1);

    /* emit one intra frame every ten frames
     * check frame pict_type before passing frame
     * to encoder, if frame->pict_type is AV_PICTURE_TYPE_I
     * then gop_size is ignored and the output of encoder
     * will always be I frame irrespective to gop_size
     */
//    c->max_b_frames = 1;
    c->pix_fmt = AV_PIX_FMT_YUV420P;
    c->qmin = 0;
    c->qmax = 69;
    c->gop_size = 96;
    c->level = 40;
    c->profile = 77;
//    c->flags |= AV_CODEC_FLAG_GLOBAL_HEADER; // TODO: maybe this one (tried, looks unlikely)?

    if (codec->id == AV_CODEC_ID_H264) {
        uint8_t *value = nullptr;
        av_opt_set(c->priv_data, "preset", "ultrafast", 0);
//        ret = av_opt_set_int(c->priv_data, "rc-lookahead", 0, 0);
//        ret = av_opt_set_int(c->priv_data, "keyint_min", 9, 0);
//        ret = av_opt_set_int(c->priv_data, "keyint", 96, 0);
//        av_opt_get_int(c->priv_data, "rc-lookahead", 0, &value);
//        std::cout << value << std::endl;
    }

    /* open it */
    ret = avcodec_open2(c, nullptr, nullptr);
    if (ret < 0) {
        fprintf(stderr, "Could not open codec: %i\n", ret);
        exit(1);
    }

    f = fopen(filename, "wb");
    if (!f) {
        fprintf(stderr, "Could not open %s\n", filename);
        exit(1);
    }


    for (int i = 0; i < 80; i++) {
        fflush(stdout);
        char file_name[48];
        snprintf(file_name, 48, R"(c:\dev\tasks\2437932\experiment\frames\%04d.raw)", i + 1);
        const std::vector<uint8_t> bytes = read_all_bytes(file_name);
        AVFrame *frame = av_frame_alloc();
        if (!frame) {
            fprintf(stderr, "Could not allocate video frame\n");
            exit(1);
        }
        frame->format = c->pix_fmt;
        frame->width = c->width;
        frame->height = c->height;

        if (av_image_fill_arrays(frame->data, frame->linesize, &bytes[0], c->pix_fmt, frame->width, frame->height, 1) < 0)
            exit(1);

        frame->pts = i * 1000;

        /* encode the image */
        encode(c, frame, f);
        av_frame_free(&frame);
    }
    // TODO: interesting! From logs you can see that it only writes packet 0 after frame 13 was sent, i.e., it buffers the frames out. What does the Transcoder do?

    /* flush the encoder */
    encode(c, nullptr, f);

    /* Add sequence end code to have a real MPEG file.
       It makes only sense because this tiny examples writes packets
       directly. This is called "elementary stream" and only works for some
       codecs. To create a valid file, you usually need to write packets
       into a proper file format or protocol; see muxing.c.
     */
    if (codec->id == AV_CODEC_ID_MPEG1VIDEO || codec->id == AV_CODEC_ID_MPEG2VIDEO)
        fwrite(endcode, 1, sizeof(endcode), f);
    fclose(f);

    avcodec_free_context(&c);
//    av_packet_free(&pkt);

    return 0;
}