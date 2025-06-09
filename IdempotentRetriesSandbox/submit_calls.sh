#!/bin/bash

for i in {0..11}; do
    curl -v -X PUT "http://localhost:5110/calls/123e4567-e89b-12d3-a456-42661417400$(printf "%02d" "$i")" -H "Content-Type: application/json" &
done
wait