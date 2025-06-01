```shell
docker run --name redis_instance -d -p 6379:6379 redis
```

```shell
curl -X POST "http://localhost:5032/sessions/123e4567-e89b-12d3-a456-426614174000" -H "Content-Type: application/json"
curl -X POST "http://localhost:5032/sessions/123e4567-e89b-12d3-a456-426614174001" -H "Content-Type: application/json"

curl -X DELETE "http://localhost:5032/sessions/123e4567-e89b-12d3-a456-426614174000" -H "Content-Type: application/json"
curl -X DELETE "http://localhost:5032/sessions/123e4567-e89b-12d3-a456-426614174001" -H "Content-Type: application/json"
```