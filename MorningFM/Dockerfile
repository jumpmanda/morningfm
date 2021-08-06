# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /build

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
WORKDIR /src
RUN echo 'hello world'
COPY ["MorningFM/MorningFM.csproj", "MorningFM/"]
COPY ["MorningFM.Logic/MorningFM.Logic.csproj", "MorningFM.Logic/"]
RUN dotnet restore "MorningFM/MorningFM.csproj"

# copy everything else and build app
COPY . .
WORKDIR /build
RUN dotnet publish "../src/MorningFM/MorningFM.csproj" -c release -o published --no-cache

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /build/published ./
ENTRYPOINT ["dotnet", "MorningFM.dll"]