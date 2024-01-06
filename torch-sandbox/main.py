import torch


def assert_gpu_available():
    assert torch.cuda.is_available(), "GPU is not available"


if __name__ == '__main__':
    assert_gpu_available()
    print("Ready to go")
    x = torch.rand(2, 3)
    print(x)
