#!/bin/sh

nohup python3 -m http.server &

xvfb-run --auto-servernum node index.js
