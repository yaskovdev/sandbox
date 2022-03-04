# Capturing Server

## Local Run

In the project directory run:

```shell
python3 -m http.server # starts HTTP server that exposes the test HTML page
node index.js # starts HTTP server that is doing actual capturing
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
   -d '{ "urlOfWebPageToCapture": "http://localhost:8000/", "webPageWidth": 800, "webPageHeight": 600 }' \
   --output ./recording.webm
```

Then check the `recording.webm` file in the same directory.
