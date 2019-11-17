FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ApiServer/ApiServer.csproj ApiServer/
RUN dotnet restore ApiServer/ApiServer.csproj
COPY . .
WORKDIR /src/ApiServer
RUN dotnet build ApiServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ApiServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApiServer.dll"]
