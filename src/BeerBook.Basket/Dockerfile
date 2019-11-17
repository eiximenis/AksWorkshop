FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BeerBook.Basket/BeerBook.Basket.csproj", "BeerBook.Basket/"]
RUN dotnet restore "BeerBook.Basket/BeerBook.Basket.csproj"
COPY . .
WORKDIR "/src/BeerBook.Basket"
RUN dotnet build "BeerBook.Basket.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BeerBook.Basket.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeerBook.Basket.dll"]