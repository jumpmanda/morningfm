#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

FROM node:12-alpine as build-node
WORKDIR /MorningFM/ClientApp
COPY /MorningFM/ClientApp/package.json .
COPY /MorningFM/ClientApp/package-lock.json .
RUN npm install
COPY /MorningFM/ClientApp/ . 
RUN npm run build 

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ENV BuildingDocker true
WORKDIR /src
COPY ["MorningFM/MorningFM.csproj", "MorningFM/"]
COPY ["MorningFM.Logic/MorningFM.Logic.csproj", "MorningFM.Logic/"]
#COPY *.csproj ./
RUN dotnet restore "MorningFM/MorningFM.csproj"
COPY . .
WORKDIR "/src/MorningFM"
RUN dotnet build "MorningFM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MorningFM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-node . ./ClientApp
ENTRYPOINT ["dotnet", "MorningFM.dll"]