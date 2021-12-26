#include <iostream>
#include <chrono>
#include "entity.h"
#include <cstdio>
#include <memory>

using std::unique_ptr;
using std::make_unique;
using std::shared_ptr;
using std::make_shared;
using std::cout;
using std::endl;
using std::chrono::duration_cast;
using std::chrono::microseconds;
using std::chrono::system_clock;
using std::string;

static string generate_file_name(const char *directory) {
    uint64_t microseconds_since_epoch = duration_cast<microseconds>(system_clock::now().time_since_epoch()).count();
    return string(directory) + std::to_string(microseconds_since_epoch);
}

bool function_that_returns_false() {
    int status = 0;
    return !!status;
}

//typedef struct pair {
//    int x;
//    int y;
//} pair;
//
//// Allocate on the stack if possible. It is not possible if you need a bigger scope, or you need to load really much data (megabytes of data).
//pair function_that_returns_pair_by_value() {
//    pair p = {0, 1};
//    return p;
//}

void string_termination_sandbox() {
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
}

//void return_by_value_sandbox() {
//    for (int i = 0; i < 100000; i++) {
//        const pair p = function_that_returns_pair_by_value();
//        const pair q = {10, 20};
//        printf("p.x=%d, p.y=%d\n", p.x, p.y);
//        printf("q.x=%d, q.y=%d\n", q.x, q.y);
//    }
//}

void unique_ptr_sandbox() {
    unique_ptr<entity> unique_entity = make_unique<entity>();
    unique_entity->print_name();
}

void create_many_files_with_unique_names() {
    for (int i = 0; i < 1000; ++i) {
        const string file_name = generate_file_name("/Users/yaskovdev/dev/files/");
        cout << file_name << endl;
        const char *null_terminated_file_name = file_name.c_str();
        // Note: file name_ you pass to fopen should be null-terminated. c_str() returns a pointer to null-terminated array of characters.
        FILE *file = fopen(null_terminated_file_name, "w");
        if (file == nullptr) {
            cout << "Cannot open file with name " << null_terminated_file_name << endl;
            return;
        }
        fprintf(file, "Iteration %d", i);
        fclose(file);
    }
}

int main(int argc, char **argv) {
//    int const *const ints = new int[5];
//    ints[0] = 5;
    {
        shared_ptr<entity> ptr_to_entity = make_shared<entity>();
        {
            shared_ptr<entity> another_ptr_to_entity = ptr_to_entity;
            another_ptr_to_entity->print_name();
            cout << "Going to exit inner block" << endl;
        }
        cout << "Exited inner block" << endl;
        ptr_to_entity->print_name();
        cout << "Going to exit outer block" << endl;
    }
    cout << "Exited outer block" << endl;

    int word_size = 5;
    char *word = new char[word_size + 1];
    char *current = word;
    while (current - word < word_size) {
        *current = 'a';
        current += 1; // pointers increment according to the size of their type
    }
    *current = '\0';
    cout << word << endl;

    entity e;
    cout << "y before increment " << std::to_string(e.count.y) << endl;
    e.increment();
    cout << "y after increment " << std::to_string(e.count.y) << endl;

    return 0;
}
