FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY CoffeShop.sln ./
COPY CoffeShop/CoffeShop.csproj CoffeShop/
RUN dotnet restore CoffeShop/CoffeShop.csproj

COPY . .
WORKDIR /src/CoffeShop
RUN dotnet publish CoffeShop.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "CoffeShop.dll"]
