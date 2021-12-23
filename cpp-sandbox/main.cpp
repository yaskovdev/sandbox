#include <iostream>
#include "chrono"
#include "string"
#include "sstream"
#include <cstdio>

static std::string generate_file_name(const char *directory) {
    const long long ms = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
    std::stringstream stream;
    stream << directory << ms;
    return stream.str();
}

bool function_that_returns_bool() {
    int status = 0;
    return !!status;
}

int main(int argc, char **argv) {
    if (function_that_returns_bool())
        std::cout << "true" << std::endl;
    else
        std::cout << "false" << std::endl;
//    for (int i = 0; i < 10000; i++) {
////        std::string file_name = generate_file_name("/Users/yaskovdev/dev/frames/");
////        const long long ms = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count();
////        std::stringstream stream;
////        stream << "/Users/yaskovdev/dev/frames/" << ms;
////        const std::string string = stream.str();
////        const char *file_name = string.c_str();
////        std::cout << "File name is " << file_name << std::endl;
//        char *word = new char[10];
//        for (int j = 0; j < 10; ++j) {
//            word[j] = 'a';
//        }
//        printf("Word is %s\n", word);
////        FILE *f = fopen(file_name, "wb");
////        fwrite(word, 1, 10, f);
////        fclose(f);
//        delete[] word;
//    }
    return 0;
}
