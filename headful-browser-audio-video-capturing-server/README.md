# Capturing Server

## Local Run

In the project directory run:

```shell
node index.js
```

## Docker Image Build And Run

In the project directory run:

```shell
docker build . -t yaskovdev/capturing-server
docker run -p 8080:8080 -d yaskovdev/capturing-server
```

## Docker Image Push To Docker Registry

```shell
docker image push yaskovdev/capturing-server
```

## Docker Image Push To Azure Registry

```shell
docker login -u CapturingServerContainerRegistry capturingservercontainerregistry.azurecr.io
docker image tag yaskovdev/capturing-server capturingservercontainerregistry.azurecr.io/capturingservercontainerregistry/capturing-server
docker image push capturingservercontainerregistry.azurecr.io/capturingservercontainerregistry/capturing-server
```

## Create Pod And Service In Kubernetes

az aks update -n CapturingServerAks -g HeadfulCapturingServer --attach-acr CapturingServerContainerRegistry

https://docs.microsoft.com/en-us/azure/aks/http-application-routing

```shell
kubectl create -f deployment/pod.yaml
kubectl create -f deployment/service.yaml
curl http://localhost:32674/status
```

## Capturing The Sample Web Page

In your terminal run:

```shell
curl -v http://localhost:8080/captures \
   -H 'Content-Type: application/json' \
   -d '{ "urlOfWebPageToCapture": "https://yaskovdev.github.io/video-and-audio-capturing-test/", "webPageWidth": 800, "webPageHeight": 600, "frameRate": 8, "durationInSeconds": 4 }' \
   --output ./recording.webm
```

Then check the `recording.webm` file in the same directory.

## Monitoring CPU And RAM In Kubernetes

```shell
watch kubectl top node
```
