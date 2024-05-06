import math

import keras_core as keras
import numpy as np
from PIL import Image

import mnist_loader


def build_model():
    return keras.Sequential([
        keras.layers.Input(shape=(784,)),
        keras.layers.Dense(units=16),
        keras.layers.Dense(units=16),
        keras.layers.Dense(units=10)
    ])


print("Ready to go")

model = build_model()

model.summary()

data = mnist_loader.load_data()
image_index = 2
image = data[image_index][0][0]
Image.fromarray((255 * np.reshape(image, (math.isqrt(image.size), -1))).astype('uint8')).show()
