import os

import numpy as np
import torch

import mnist_loader
from simple_common import build_model, cwd, BATCH_SIZE

model = build_model(BATCH_SIZE)

model.summary()

data = mnist_loader.load_data()

training_data = data[0]
training_inputs = training_data[0]
training_targets = training_data[1]

loss_fn = torch.nn.CrossEntropyLoss(reduction='mean')

optimizer = torch.optim.SGD(model.parameters(), lr=1e-2, momentum=0.9)

for i in range(1000):
    input_batch = np.reshape(training_inputs, [BATCH_SIZE, -1])
    target_batch = training_targets

    prediction = model(input_batch)
    loss = loss_fn(prediction, torch.LongTensor(target_batch).cuda())

    loss.backward()
    optimizer.step()
    optimizer.zero_grad()
    if i % 10 == 0:
        print(i, loss.item())

file_name = "simple_model.keras"
model.save(os.path.join(cwd, "models", file_name))
print("Model has been saved as", file_name)
