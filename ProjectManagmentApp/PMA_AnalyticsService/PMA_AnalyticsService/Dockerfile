#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PMA_AnalyticsService/PMA_AnalyticsService/PMA_AnalyticsService.csproj", "PMA_AnalyticsService/PMA_AnalyticsService/"]
RUN dotnet restore "PMA_AnalyticsService/PMA_AnalyticsService/PMA_AnalyticsService.csproj"
COPY . .
WORKDIR "/src/PMA_AnalyticsService/PMA_AnalyticsService"
RUN dotnet build "PMA_AnalyticsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PMA_AnalyticsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PMA_AnalyticsService.dll"]