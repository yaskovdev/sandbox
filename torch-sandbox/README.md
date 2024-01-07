# PyTorch Sandbox

1. Install Conda from [the official website](https://www.anaconda.com/download/#windows).
2. Create a new Conda environment using `conda create -n torch python=3.8`. Note: although the PyTorch guide says "
   Latest PyTorch requires Python 3.8 or later", the latest Python may be not supported. Pick one that is definitely
   supported.
3. Activate the new environment with `conda activate torch`.
4. Follow [this guide](https://pytorch.org/get-started/locally/) to install PyTorch.
5. If you are using PyCharm, make sure to [pick the Conda environment](https://stackoverflow.com/a/46133678/1862286) you
   have just created.
6. `pip install keras-core==0.1.2`
7. Create new environment variable `PROTOCOL_BUFFERS_PYTHON_IMPLEMENTATION` and set it to `python`.
8. Make sure that `backend` is `torch` in `keras.json`.