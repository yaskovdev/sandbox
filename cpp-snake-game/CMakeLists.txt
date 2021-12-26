cmake_minimum_required(VERSION 3.20)
project(cpp_snake_game)

set(CMAKE_CXX_STANDARD 14)
set(CMAKE_CXX_FLAGS "-D__STDC_CONSTANT_MACROS")

find_package(SDL2 REQUIRED)

add_executable(cpp_snake_game main.cpp game.cpp game.h enemy.cpp enemy.h pair.cpp pair.h)
target_include_directories(cpp_snake_game PRIVATE ${SDL2_INCLUDE_DIRS})
target_link_libraries(cpp_snake_game PRIVATE ${SDL2_LIBRARIES})
