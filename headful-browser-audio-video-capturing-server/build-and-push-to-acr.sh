#!/bin/sh

TAG=$(git rev-parse --short HEAD)
IMAGE=capturingservercontainerregistry.azurecr.io/capturingservercontainerregistry/capturing-server:$TAG
docker build . -t "$IMAGE"
docker image push "$IMAGE"

echo "Image $IMAGE is pushed. Do not forget to update image in pod.yaml before deployment!"
