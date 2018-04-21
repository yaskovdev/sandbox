#!/usr/bin/env bash

curl -v -H "Content-Type: application/json" --data @./role.json http://localhost:8080/roles

curl -v -H "Content-Type: application/json" --data @./updated-role.json http://localhost:8080/roles/CODE
