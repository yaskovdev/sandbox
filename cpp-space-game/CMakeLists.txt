cmake_minimum_required(VERSION 3.20)
project(cpp_space_game)

set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_FLAGS "-D__STDC_CONSTANT_MACROS")

find_package(SDL2 REQUIRED)

add_executable(cpp_space_game main.cpp game.cpp game.h space_object.cpp space_object.h pair.cpp pair.h color.cpp color.h player.cpp player.h game_clock.cpp game_clock.h moving_space_object.cpp moving_space_object.h weapon.cpp weapon.h)
target_include_directories(cpp_space_game PRIVATE ${SDL2_INCLUDE_DIRS})
target_link_libraries(cpp_space_game PRIVATE ${SDL2_LIBRARIES})
