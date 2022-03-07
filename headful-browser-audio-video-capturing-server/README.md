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
docker run -p 49160:8080 -d yaskovdev/capturing-server
```

## Capturing The Sample Web Page

In your terminal run:

```shell
curl -v http://localhost:49160/ \
   -H 'Content-Type: application/json' \
   -d '{ "urlOfWebPageToCapture": "https://yaskovdev.github.io/video-and-audio-capturing-test/", "webPageWidth": 800, "webPageHeight": 600, "durationInSeconds": 10 }' \
   --output ./recording.webm
```

Then check the `recording.webm` file in the same directory.
