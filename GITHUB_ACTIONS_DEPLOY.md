# GitHub Actions: CI and deployment to this computer

This repository uses two runners for a push to `master`:

1. A GitHub-hosted runner restores packages, builds the solution, runs unit tests, and publishes a Docker image to GitHub Container Registry (GHCR).
2. A self-hosted runner on this computer pulls that exact image and starts the API and SQL Server with `docker-compose.deploy.yml`.

The deployed API image is tagged with the commit SHA. This prevents the deployment job from accidentally running an older `latest` image.

## One-time GitHub setup

### 1. Register the self-hosted runner

In the GitHub repository, open:

```text
Settings -> Actions -> Runners -> New self-hosted runner
```

Choose Windows x64 and follow GitHub's displayed commands in a dedicated directory outside this repository. Add the custom label:

```text
bankingapp-deploy
```

Start the runner. It must stay running and Docker Desktop must be running whenever a deployment should happen.

Use this runner only for this repository. Do not configure a self-hosted runner for a public repository that accepts pull requests from unknown forks.

### 2. Add repository secrets

Open:

```text
Settings -> Secrets and variables -> Actions
```

Create these repository secrets:

| Secret | Purpose |
| --- | --- |
| `MSSQL_SA_PASSWORD` | SQL Server `sa` password. It must have SQL Server's required complexity. |
| `JWT_KEY` | JWT signing key used by the API. |

Optionally create the repository variable `JWT_ISSUER`. If omitted, the workflow uses `bankingapp`.

## First deployment

1. Commit and push the workflow and `docker-compose.deploy.yml` to `master`.
2. In the Actions tab, open the `CI and local Docker deploy` workflow.
3. Check that `Build, test and publish image` succeeds.
4. Check that `Deploy on local self-hosted runner` is picked up by your runner and succeeds.
5. Open `http://localhost:8080/swagger`.

The SQL Server data is stored in the named volume `sqlserver_data`, so normal deploys reuse the existing database.

## What each push does

```text
push to master
  -> GitHub runner: restore, build, test
  -> GitHub runner: Docker build and push to ghcr.io
  -> local runner: docker compose pull
  -> local runner: docker compose up -d
```

The local runner does not build the API image. It pulls the image produced by CI, which keeps the deployment path close to a normal cloud deployment.

## Local versus deployment Compose files

- `docker-compose.yml` is for local manual development and builds the API from source.
- `docker-compose.deploy.yml` is for the GitHub Actions deployment and pulls the API image from GHCR.

Do not put real passwords or JWT keys in either Compose file. The deployment file receives them only from GitHub Secrets.
