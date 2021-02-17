#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DBServer/DBServer.csproj", "DBServer/"]
RUN dotnet restore "DBServer/DBServer.csproj"
COPY . .
WORKDIR "/src/DBServer"
RUN dotnet build "DBServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DBServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["DBServer/personen.db", "/app/publish/"]
COPY ["DBServer/personen.db", "/app"]
COPY ["DBServer/personen.db", "/"]
ENTRYPOINT ["dotnet", "DBServer.dll"]