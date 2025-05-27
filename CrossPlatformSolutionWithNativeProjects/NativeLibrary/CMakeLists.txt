cmake_minimum_required(VERSION 3.10)
project(NativeLibrary)

set(CMAKE_CXX_STANDARD 14)

# Создаём библиотеку из исходных файлов
add_library(NativeLibrary SHARED api.cpp api.h)

# Адаптация экспорта символов для кросс-платформенности
if(WIN32)
    target_compile_definitions(NativeLibrary PRIVATE 
        CPPDYNAMICLIBRARYTEMPLATE_EXPORTS 
        _WINDOWS 
        _USRDLL)
else()
    # На macOS не нужен __declspec(dllexport)
    target_compile_definitions(NativeLibrary PRIVATE 
        "DECLSPEC=")
endif()

# Настройка суффикса для библиотеки
if(APPLE)
    set_target_properties(NativeLibrary PROPERTIES
        SUFFIX ".dylib")
endif()