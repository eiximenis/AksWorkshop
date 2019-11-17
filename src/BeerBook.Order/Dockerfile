FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BeerBook.Order/BeerBook.Order.csproj", "BeerBook.Order/"]
RUN dotnet restore "BeerBook.Order/BeerBook.Order.csproj"
COPY . .
WORKDIR "/src/BeerBook.Order"
RUN dotnet build "BeerBook.Order.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BeerBook.Order.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeerBook.Order.dll"]