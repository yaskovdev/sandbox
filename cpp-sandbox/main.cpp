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

// Allocate on the stack if possible. It is not possible if you need a bigger scope, or you need to load really much data (megabytes of data).
int main(int argc, char **argv) {
    for (int i = 0; i < 10000; i++) {
        int word_size = 10;
        char *heap_word = new char[word_size];
        for (int j = 0; j < word_size; ++j) {
            heap_word[j] = 'a';
        }
        printf("Heap word is %.*s\n", word_size, heap_word);
        printf("Heap word is %.10s\n", heap_word);
        delete[] heap_word;

        char *null_terminated_heap_word = new char[word_size + 1];
        for (int j = 0; j < word_size; ++j) {
            null_terminated_heap_word[j] = 'a';
        }
        null_terminated_heap_word[word_size] = '\0';
        printf("Null-terminated heap word is %s\n", null_terminated_heap_word);

        const char *string_literal_data_segment_word = "aaaaaaaaaa"; // string literals are stored in data segment: https://en.wikipedia.org/wiki/Data_segment
        printf("String literal heap word is %s\n", string_literal_data_segment_word);
    }
    return 0;
}
