import os

import keras_core as keras
import numpy as np
import regex as re
import torch
from torch.nn.functional import softmax

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
    shape = (100,)  # TODO: check the shape parameter docs, it was (None, ) previously
    stateful = False  # TODO: check why setting it to True fails the call to loss.backward()
    return keras.Sequential([
        keras.layers.Input(shape=shape, batch_size=batch_size),
        keras.layers.Embedding(vocabulary_size, embedding_dim),
        keras.layers.LSTM(rnn_units, return_sequences=True, recurrent_initializer='glorot_uniform', recurrent_activation='sigmoid', stateful=stateful),
        keras.layers.Dense(vocabulary_size)
    ])


if __name__ == '__main__':
    torch.autograd.set_detect_anomaly(True)
    # assert_gpu_available()
    print("Ready to go")
    # m = torch.nn.LogSoftmax(dim=-1)
    # print(softmax(torch.Tensor([0, 0, 0, 1])))

    # model = resnet18(weights=ResNet18_Weights.DEFAULT)
    # data = torch.rand(1, 3, 64, 64)
    # labels = torch.rand(1, 1000)
    # prediction = model(data)  # forward pass
    # loss = (prediction - labels).sum()
    # loss.backward()  # backward pass
    # print("Done")

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

    loss_fn = torch.nn.CrossEntropyLoss(reduction='mean')  # TODO: reduction
    optim = torch.optim.SGD(model.parameters(), lr=1e-2, momentum=0.9)
    for i in range(100):
        x, y = get_batch(vectorized_songs, seq_length=100, batch_size=32)
        optim.zero_grad()
        prediction = model(x)
        loss = loss_fn(prediction.permute((0, 2, 1)).cpu(), torch.from_numpy(y).long())  # TODO: use CUDA
        if i % 1 == 0:
            print(i, loss.item())
        loss.backward()
        optim.step()

    x_answer, y_answer = get_batch(vectorized_songs, seq_length=100, batch_size=32)
    answer = model(x_answer)
    print("Input shape:      ", x_answer.shape, " # (batch_size, sequence_length)")
    print("Prediction shape: ", answer.shape, "# (batch_size, sequence_length, vocab_size)")
    # softmax(answer[0])
    # TODO: check if this is the correct replacement of tf.random.categorical
    # TODO: check softmax documentation, there is also log_softmax and torch.nn.LogSoftmax(dim=0)
    sampled_indices = torch.squeeze(torch.multinomial(softmax(answer[0], dim=0), 1, replacement=True)).cpu()
    print("Prediction: ", sampled_indices)

    print("Input: \n", repr("".join(idx2char[x_answer[0]])))
    print("Target: \n", repr("".join(idx2char[y_answer[0]])))
    print("Next Char Predictions: \n", repr("".join(idx2char[sampled_indices])))
