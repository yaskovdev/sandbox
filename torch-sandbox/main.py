import os

import keras_core as keras
import numpy as np
import regex as re
import torch

cwd = os.path.dirname(__file__)


def assert_gpu_available():
    assert torch.cuda.is_available(), "GPU is not available"


def extract_song_snippet(text):
    pattern = '(^|\n\n)(.*?)\n\n'
    search_results = re.findall(pattern, text, overlapped=True, flags=re.DOTALL)
    songs = [song[1] for song in search_results]
    print("Found {} songs in text".format(len(songs)))
    return songs


def load_training_data():
    with open(os.path.join(cwd, "data", "irish.abc"), "r") as f:
        text = f.read()
    return extract_song_snippet(text)


def vectorize_string(string, character_to_index):
    return np.array([character_to_index[character] for character in string])


def get_batch(vectorized_songs, seq_length, batch_size):
    # the length of the vectorized songs string
    n = vectorized_songs.shape[0] - 1
    # randomly choose the starting indices for the examples in the training batch
    idx = np.random.choice(n - seq_length, batch_size)

    input_batch = [vectorized_songs[i:i + seq_length] for i in idx]
    output_batch = [vectorized_songs[i + 1:i + seq_length + 1] for i in idx]

    # x_batch, y_batch provide the true inputs and targets for network training
    x_batch = np.reshape(input_batch, [batch_size, seq_length])
    y_batch = np.reshape(output_batch, [batch_size, seq_length])
    return x_batch, y_batch


def build_model(vocabulary_size, embedding_dim, rnn_units, batch_size):
    return keras.Sequential([
        keras.layers.Input(shape=(None,), batch_size=batch_size),  # TODO: check the shape parameter docs
        # Layer 1: Embedding layer to transform indices into dense vectors
        #   of a fixed embedding size
        keras.layers.Embedding(vocabulary_size, embedding_dim),

        # Layer 2: LSTM with `rnn_units` number of units.
        keras.layers.LSTM(rnn_units, return_sequences=True, recurrent_initializer='glorot_uniform', recurrent_activation='sigmoid', stateful=True),

        # Layer 3: Dense (fully-connected) layer that transforms the LSTM output
        #   into the vocabulary size.
        keras.layers.Dense(vocabulary_size),
    ])


def compute_loss(labels, logits):
    loss = torch.nn.CrossEntropyLoss()
    return loss(labels, logits)


if __name__ == '__main__':
    # assert_gpu_available()
    print("Ready to go")
    songs = load_training_data()
    example_song = songs[0]

    # Join our list of song strings into a single string containing all songs
    songs_joined = "\n\n".join(songs)

    # Find all unique characters in the joined string
    vocab = sorted(set(songs_joined))
    print("There are", len(vocab), "unique characters in the dataset")

    char2idx = {u: i for i, u in enumerate(vocab)}

    idx2char = np.array(vocab)

    print('{')
    for char, _ in zip(char2idx, range(20)):
        print('  {:4s}: {:3d},'.format(repr(char), char2idx[char]))
    print('  ...\n}')

    vectorized_songs = vectorize_string(songs_joined, char2idx)

    print(vectorized_songs)

    print('{} ---- characters mapped to int ----> {}'.format(repr(songs_joined[:10]), vectorized_songs[:10]))

    x_batch, y_batch = get_batch(vectorized_songs, seq_length=5, batch_size=1)

    for i, (input_idx, target_idx) in enumerate(zip(np.squeeze(x_batch), np.squeeze(y_batch))):
        print("Step {:3d}".format(i))
        print("  input: {} ({:s})".format(input_idx, repr(idx2char[input_idx])))
        print("  expected output: {} ({:s})".format(target_idx, repr(idx2char[target_idx])))

    model = build_model(len(vocab), embedding_dim=256, rnn_units=1024, batch_size=32)

    model.summary()

    x, y = get_batch(vectorized_songs, seq_length=100, batch_size=32)
    prediction = model(x)
    print("Input shape:      ", x.shape, " # (batch_size, sequence_length)")
    print("Prediction shape: ", prediction.shape, "# (batch_size, sequence_length, vocab_size)")
    # TODO: check if this is the correct replacement of tf.random.categorical
    sampled_indices = torch.squeeze(torch.multinomial(torch.exp(prediction[0]), 1, replacement=True)).numpy()
    print("Prediction: ", sampled_indices)

    print("Input: \n", repr("".join(idx2char[x[0]])))
    print("Next Char Predictions: \n", repr("".join(idx2char[sampled_indices])))

    example_batch_loss = compute_loss(prediction, torch.from_numpy(y))
