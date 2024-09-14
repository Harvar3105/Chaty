FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Chaty/Chaty.csproj", "Chaty/"]
RUN dotnet restore "Chaty/Chaty.csproj"
COPY . .
WORKDIR "/src/Chaty"
RUN dotnet build "Chaty.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chaty.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chaty.dll"]
