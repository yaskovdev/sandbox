import keras_core as keras
import numpy as np
import torch
from torch.nn.functional import softmax

import mnist_loader

BATCH_SIZE = 64


def split_into_batches(array, batch_size):
    return np.array_split(array, range(batch_size, len(array), batch_size))


def build_model(batch_size):
    return keras.Sequential([
        keras.layers.Input(shape=(784,), batch_size=batch_size),
        keras.layers.Dense(units=16),
        keras.layers.Dense(units=16),
        keras.layers.Dense(units=10)
    ])


print("Ready to go")

model = build_model(BATCH_SIZE)

model.summary()

data = mnist_loader.load_data()
# image_index = 2
# image = data[image_index][0][0]
# Image.fromarray((255 * np.reshape(image, (math.isqrt(image.size), -1))).astype('uint8')).show()

training_data = data[0]
training_inputs = training_data[0]
training_targets = training_data[1]
training_input_batches = split_into_batches(training_inputs, BATCH_SIZE)
training_target_batches = split_into_batches(training_targets, BATCH_SIZE)

loss_fn = torch.nn.CrossEntropyLoss(reduction='mean')

optim = torch.optim.SGD(model.parameters(), lr=1e-2, momentum=0.9)

for i in range(5000):
    indexes = np.random.choice(len(training_inputs), BATCH_SIZE)
    input_batch = np.reshape([training_inputs[j] for j in indexes], [BATCH_SIZE, -1])
    target_batch = [training_targets[j] for j in indexes]
    optim.zero_grad()
    prediction = model(input_batch)
    loss = loss_fn(prediction, torch.LongTensor(target_batch).cuda())
    print(i, loss.item())
    loss.backward()
    optim.step()

validation_data = data[1]
validation_inputs = validation_data[0]
validation_targets = validation_data[1]
validation_input_batches = split_into_batches(validation_inputs, BATCH_SIZE)
validation_target_batches = split_into_batches(validation_targets, BATCH_SIZE)

for i in range(min(100, len(validation_input_batches))):
    input_batch = validation_input_batches[i]
    target_batch = validation_target_batches[i]
    prediction = model(input_batch)
    distribution = softmax(prediction, dim=1)
    answer = torch.squeeze(torch.multinomial(distribution, 1, replacement=True))
    right_answers = np.sum(answer.cpu().numpy() == target_batch)
    print("Got ", right_answers, " out of ", len(target_batch))
