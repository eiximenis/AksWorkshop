FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster  AS build
WORKDIR /src
COPY ["BeerBook.Web/BeerBook.Web.csproj", "BeerBook.Web/"]
RUN dotnet restore "BeerBook.Web/BeerBook.Web.csproj"
COPY . .
WORKDIR "/src/BeerBook.Web"
RUN dotnet build "BeerBook.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BeerBook.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeerBook.Web.dll"]