# Docker - pierwszy krok dla BankingApp

Ten projekt najlepiej poznawać w **2 kontenerach**:

- `api` - Twoja aplikacja ASP.NET Core
- `db` - SQL Server

To jeszcze nie są mikroserwisy. To po prostu jedna aplikacja uruchamiana w przewidywalnym środowisku.

## Co zostało przygotowane

- `Dockerfile` - buduje obraz aplikacji
- `docker-compose.yml` - uruchamia API i bazę razem
- automatyczne `EF Core migrations` przy starcie aplikacji

## Jak uruchomić

W katalogu projektu:

```powershell
docker compose up --build
```

API będzie dostępne pod:

```text
http://localhost:8080
```

Swagger:

```text
http://localhost:8080/swagger
```

## Jak to działa

### 1. `Dockerfile`

Obraz buduje aplikację w dwóch etapach:

- `sdk` - przywraca paczki i publikuje aplikację
- `aspnet` - uruchamia już gotowy, lżejszy build

To jest tzw. **multi-stage build** i jest bardzo typowe w .NET.

### 2. `docker-compose.yml`

Compose uruchamia dwa serwisy:

- `api`
- `db`

Najważniejsza rzecz: w kontenerach nie używamy `DESKTOP-...` jako serwera bazy.  
Zamiast tego API łączy się z bazą po nazwie serwisu:

```text
Server=db,1433
```

`db` to nazwa usługi z Compose i Docker sam rozwiązuje ją w swojej sieci.

### 3. Konfiguracja przez zmienne środowiskowe

W Compose ustawiamy:

- `ConnectionStrings__DefaultConnection`
- `AuthSettings__JwtKey`
- `AuthSettings__JwtIssuer`

To ważny dockerowy nawyk: konfigurację środowiska trzymamy poza kodem.

## Przydatne komendy

Start w tle:

```powershell
docker compose up -d --build
```

Podgląd logów:

```powershell
docker compose logs -f
```

Wyłączenie:

```powershell
docker compose down
```

Wyłączenie razem z wolumenem bazy:

```powershell
docker compose down -v
```

To ostatnie usuwa też dane SQL Servera.

## Czego się tu uczysz

Na tym etapie warto zrozumieć 4 rzeczy:

1. `Dockerfile` opisuje **jak zbudować obraz**
2. `docker compose` opisuje **jak uruchomić kilka kontenerów razem**
3. kontenery gadają ze sobą po **nazwach usług**
4. konfigurację przekazujemy przez **environment variables**

## Co dalej potem

Gdy już to będzie dla Ciebie naturalne, następnym krokiem może być:

- oddzielenie bazy i API na profile środowisk
- osobny kontener do migracji
- reverse proxy
- podział na mikroserwisy dopiero wtedy, gdy pojawi się realna potrzeba
