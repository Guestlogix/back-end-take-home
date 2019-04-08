# Shortest Path Airport Routes Solution

## Gustavo Adolfo Costa Borges

First of all, thanks for the opportunity. I'm sorry I could not work on this right after the interview, but I got sicker and was in bed the whole week. I thought that to be fair, I would have to do it in 2 days as asked, even if I started later. I hope that you enjoy my solution as much as I did making it !

## Considerations

This was a very fun challenge that hooked me up. Once I have figured out it was a Graph problem and that this was not a whiteboard-interview style, but more related to real life, I started looking for a Graph Database that had enough libraries and documentation in python. It was my first Graph project, so much of my time was learning `Neo4j` and `Cypher` query language, but I loved it.

I noticed from the interview that CI/CD is in place at Guestlogix, so I tried to cover the most important parts and focus on integration tests. The endpoints are documented with Swagger and a Swagger UI is also provided with the project at `http://localhost:5000/apidocs`.

From the _user stories_ I couldn't figure who was the _user_ consuming the api. With the actor role being unknown, I created an entity that returns minimal information to render a small ui or feed another aplication with the primary keys to retrive the full object on the proper(yet to be implemented) resource endpoint.

## How to run

The application and tests are containerized, specially because of the dependency to `Neo4j` database server. To simplify everything you can just use `make`. The database does not persist and once shutdown it will loose everything. This is intentional for the purpose of presenting. You can edit `docker-compose.yml` file and add a volume mapping for data persistace from `$HOME/neo4j/data`.

### Test
```bash
make test
```

### Seed and run server
```bash
make run
```

### Stop server and shutdown database
```bash
make stop
```
