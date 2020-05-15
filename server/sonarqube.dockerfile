FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster

# Install OpenJDK-11
RUN apt-get update && \
    apt-get install -y openjdk-11-jdk && \
    apt-get install -y ant && \
    apt-get clean;

# Fix certificate issues
RUN apt-get update && \
    apt-get install ca-certificates-java && \
    apt-get clean && \
    update-ca-certificates -f;

# Setup JAVA_HOME
ENV JAVA_HOME /usr/lib/jvm/java-11-openjdk-amd64/
RUN export JAVA_HOM

# Install global dotnet-sonarscanner
RUN dotnet tool install --global dotnet-sonarscanner --version 4.9.0

# Add global tools folder to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copy solution file and project file
WORKDIR /src
COPY *.sln ./
COPY ["CoastlineServer.Service/CoastlineServer.Service.csproj", "CoastlineServer.Service/"]
COPY ["CoastlineServer.Service.Testing/CoastlineServer.Service.Testing.csproj", "CoastlineServer.Service.Testing/"]
COPY ["CoastlineServer.Repository/CoastlineServer.Repository.csproj", "CoastlineServer.Repository/"]
COPY ["CoastlineServer.Repository.Testing/CoastlineServer.Repository.Testing.csproj", "CoastlineServer.Repository.Testing/"]
COPY ["CoastlineServer.DAL/CoastlineServer.DAL.csproj", "CoastlineServer.DAL/"]

# Restore all dependencies
RUN dotnet restore

# Copy remaining files
COPY . .
WORKDIR "/src"