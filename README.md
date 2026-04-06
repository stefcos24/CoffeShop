# CoffeShop (Docker)

This repo can run locally with **one command** using Docker Compose:

- The app runs on `http://localhost:8080`
- SQL Server runs on `localhost:1433` (persistent volume)

## Prereqs

- Docker Desktop

Note for Apple Silicon (M1/M2/M3): SQL Server runs as `linux/amd64` via emulation (configured in `docker-compose.yml`).

## Quick start (one command)

1) Create a `.env` file:

```bash
cp .env.example .env
```

2) Start everything:

```bash
docker compose up --build
```

That’s it. On first boot, the app will apply EF Core migrations automatically.

## Run locally (without Compose)

You can also run the app with `dotnet run`. The app will automatically load `.env` (if present) and build the DB connection string from:

- `DB_HOST`, `DB_PORT`, `DB_NAME`, `DB_USER`
- `MSSQL_SA_PASSWORD`

Example:

```bash
cp .env.example .env
dotnet run --project CoffeShop/CoffeShop.csproj
```

## Configuration (recommended)

- **SQL password**: set `MSSQL_SA_PASSWORD` in `.env`
- **DB settings**: set `DB_HOST`, `DB_PORT`, `DB_NAME`, `DB_USER` in `.env` (optional locally; Compose sets them automatically)

These are set via environment variables so you don’t commit secrets to `appsettings.json`.
