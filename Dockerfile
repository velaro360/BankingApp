FROM mcr.microsoft.com/dotnet/sdk:8.0.420 AS build
WORKDIR /src

COPY ["BankingApp/BankingApp.csproj", "BankingApp/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "BankingApp/BankingApp.csproj"

COPY . .
WORKDIR /src/BankingApp
RUN dotnet publish "BankingApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BankingApp.dll"]
