FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-stage
WORKDIR /src
COPY *.sln ./
COPY ["CoastlineServer.Service/CoastlineServer.Service.csproj", "CoastlineServer.Service/"]
COPY ["CoastlineServer.Repository/CoastlineServer.Repository.csproj", "CoastlineServer.Repository/"]
COPY ["CoastlineServer.Repository.Testing/CoastlineServer.Repository.Testing.csproj", "CoastlineServer.Repository.Testing/"]
COPY ["CoastlineServer.DAL/CoastlineServer.DAL.csproj", "CoastlineServer.DAL/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/CoastlineServer.Service"
RUN dotnet build -c Release -o /app/build

FROM build-stage AS publish-stage
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS production-stage
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish-stage /app/publish .
ENTRYPOINT ["dotnet", "CoastlineServer.Service.dll"]