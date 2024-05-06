import os

import keras_core as keras
import numpy as np
import regex as re
import torch
from torch.nn.functional import log_softmax, softmax


# https://www.kaggle.com/code/ryanholbrook/a-single-neuron/tutorial
def build_model():
    return keras.Sequential([
        keras.layers.Input(shape=(3,)),
        keras.layers.Dense(units=1)
    ])


print("Ready to go")

model = build_model()

model.summary()
