cmake_minimum_required(VERSION 3.20)
project(ffmpeg_sws)

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_FLAGS "-D__STDC_CONSTANT_MACROS")

find_path(AVCODEC_INCLUDE_DIR libavcodec/avcodec.h REQUIRED)
find_library(AVCODEC_LIBRARY avcodec REQUIRED)

find_path(AVFORMAT_INCLUDE_DIR libavformat/avformat.h)
find_library(AVFORMAT_LIBRARY avformat)

find_path(AVUTIL_INCLUDE_DIR libavutil/avutil.h)
find_library(AVUTIL_LIBRARY avutil)

find_path(SWSCALE_INCLUDE_DIR libswscale/swscale.h)
find_library(SWSCALE_LIBRARY swscale)

add_executable(ffmpeg_sws main.cpp)
target_include_directories(ffmpeg_sws PRIVATE ${AVCODEC_INCLUDE_DIR} ${AVFORMAT_INCLUDE_DIR} ${AVUTIL_INCLUDE_DIR} ${SWSCALE_INCLUDE_DIR})
target_link_libraries(ffmpeg_sws PRIVATE ${AVCODEC_LIBRARY} ${AVFORMAT_LIBRARY} ${AVUTIL_LIBRARY} ${SWSCALE_LIBRARY})
