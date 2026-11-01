# WKS Backend

Base ASP.NET Core / .NET 10 backend extracted from ECMC. Source namespaces use `Wks`; the project and executable remain `backend.csproj` / `backend.dll`.

## Retained

Financial is migrated from ECMC: timelines, expenses, transactions, cash, reports, analytics and home dashboard. Shared hashtag support and AI-assisted transaction descriptions are included. Configure the Intelligence provider variables to use text generation.

- Accounts, password/Google authentication, verification codes, JWT, refresh tokens, cookies and sessions.
- Profiles, actor accounts, roles, permission policies and platform administration.
- Notifications, SignalR and Redis pub/sub.
- Settings, versions/changelogs, feedback, audit actions, icons and domain management.
- Artifacts and presigned uploads/downloads backed by S3-compatible object storage.
- PostgreSQL / EF Core, Redis cache, RabbitMQ, Hangfire configuration, HTTP client, mail/Google/FCM adapters, logging, error handling, CORS and Swagger.
- All NuGet package references from the source project.

The System/Push browser subscription/key APIs, legacy Cloud file management, folders, collaboration groups and trash are omitted; file transfer uses Artifact.

Business apps are omitted: Home dashboards, commerce/POS/catalog, customers/audience, warehouse, marketing/automation/broadcasts, redirect campaigns, billing, subscriptions/quotas, wallet, signage and AI features. App-specific consumers, recurring jobs, admin operations, metadata and old migrations are removed. Account provisioning creates its default role without registering a package or generating marketing/redirect data.

## Local setup

Install the .NET 10 SDK and the EF CLI (`dotnet tool install --global dotnet-ef --version 10.0.12`).

```sh
cp Shared/Variables/.env.example Shared/Variables/.env
```

Fill in the local configuration before starting. The required infrastructure is PostgreSQL, Redis, RabbitMQ and S3-compatible object storage. The example uses localhost endpoints and placeholder credentials, with no ECMC credentials. The storage client supports `StorageUseSsl`; set it to `true` for HTTPS endpoints.

```sh
docker compose up -d
dotnet restore
dotnet ef database update
dotnet run
```

Create the `wks` bucket in the MinIO console at http://localhost:9001 before testing uploads. Its local credentials are `wks-local` / `wks-local-password`. Set the corresponding storage credentials in `.env` (already present in the example).

- API health: http://localhost:6501/api/system/health
- Swagger: http://localhost:6501/swagger
- SignalR: http://localhost:6501/hub
- Portal origin: http://localhost:6502
- RabbitMQ UI: http://localhost:15672 (local `guest` / `guest`)

Environment variables take precedence over `.env`; blank lines in the file are supported. Mail, Google OAuth and FCM require their own credentials to use those integrations. In `Development`, verification codes are logged instead of sending email. Replace the sample JWT/admin/infrastructure passwords before using any shared environment.

## Database

`InitialBase` is a fresh migration for the retained platform tables, including a `DEFAULT_PERMISSION` setting. The Financial migration grants home, application, expense and dashboard access to new accounts by default. Set the desired default permission codes through platform administration. Bootstrap elevated access through the retained role endpoint with your configured `AdminRequiredPass`; no admin account/password is seeded.

Use a new WKS database. The ECMC migration history and production data are not copied. Migrations are applied explicitly with `dotnet ef database update`; application startup does not migrate automatically. A design-time factory allows migration generation without starting Redis, RabbitMQ or the web host.

```sh
dotnet build
dotnet ef migrations has-pending-model-changes
```

`WithConsumer`, `WithSocket` and `WithJob` control consumer, socket subscriber and Hangfire behavior. Base consumers provision accounts and record audit actions; no application recurring jobs are scheduled. API and workers should use WKS-specific queues/channels/databases when deployed alongside ECMC.

## Moving existing Financial data

The repository migration moves code and schema definitions; no database data is copied automatically. Before applying `MoveFinancialToWks` to an ECMC database, populate a separate WKS database and migrate/validate the corresponding account/profile identities, the hashtags used by financial timelines, and `Cash`, `CashDescription`, `Timeline`, `TimelineTag`, `Expense`, `ExpenseTransaction` records. Preserve identifiers and foreign keys. Validate the transferred records before applying the ECMC migration, which drops those six source tables. `AddFinancial` creates the target tables and default Financial permissions in WKS.
