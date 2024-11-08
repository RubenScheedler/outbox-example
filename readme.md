# Outbox Sample
## How to run
1. Start postgres by running the `docker-compose.yml` by executing `docker-compose up`
2. View the database via adminer at `localhost:8888`.
3. Use `db` as host (= name of service in docker compose)
4. Use `postgres` as database, username and password.
5. Run the application. It will create the outbox table automatically.