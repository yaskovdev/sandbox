# Distributed Transaction Sandbox
Simulates a back end which transactionally modifies a database and then sends a JMS message to a queue. The back end uses nFlow to guarantee the eventual consistency of the data.

-Ddb.url=jdbc:mysql://localhost:3306/sandbox -Ddb.username=root -Ddb.password=root

```bash
curl -v -H "Content-Type: application/json" --data '{"type":"Some Type", "name":"Some Name"}' http://localhost:8080/notifications
```
