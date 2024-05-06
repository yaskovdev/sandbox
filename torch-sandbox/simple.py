import keras_core as keras
import numpy as np
import torch
from torch.nn.functional import softmax

import mnist_loader

batch_size = 20


def build_model():
    return keras.Sequential([
        keras.layers.Input(shape=(784,), batch_size=batch_size),
        keras.layers.Dense(units=16),
        keras.layers.Dense(units=16),
        keras.layers.Dense(units=10)
    ])


print("Ready to go")

model = build_model()

model.summary()

# data = mnist_loader.load_data()
# image_index = 2
# image = data[image_index][0][0]
# Image.fromarray((255 * np.reshape(image, (math.isqrt(image.size), -1))).astype('uint8')).show()

data = mnist_loader.load_data()
training_data = data[0]
validation_data = data[1]
validation_inputs = validation_data[0]
validation_targets = validation_data[1]

validation_input_batches = np.array_split(validation_inputs, range(batch_size, len(validation_inputs), batch_size))
validation_target_batches = np.array_split(validation_targets, range(batch_size, len(validation_targets), batch_size))

for i in range(min(10, len(validation_input_batches))):
    input_batch = validation_input_batches[i]
    target_batch = validation_target_batches[i]
    prediction = model(input_batch)
    distribution = softmax(prediction, dim=1)
    answer = torch.squeeze(torch.multinomial(distribution, 1, replacement=True))
    print(distribution)
    print(answer)
