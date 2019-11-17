FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM  mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BeerBook.Catalog/BeerBook.Catalog.csproj", "BeerBook.Catalog/"]
RUN dotnet restore "BeerBook.Catalog/BeerBook.Catalog.csproj"
COPY . .
WORKDIR "/src/BeerBook.Catalog"
RUN dotnet build "BeerBook.Catalog.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BeerBook.Catalog.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeerBook.Catalog.dll"]