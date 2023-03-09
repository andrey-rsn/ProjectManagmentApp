#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PMA_Saga/PMA_SagaService/PMA_SagaService.csproj", "PMA_Saga/PMA_SagaService/"]
RUN dotnet restore "PMA_Saga/PMA_SagaService/PMA_SagaService.csproj"
COPY . .
WORKDIR "/src/PMA_Saga/PMA_SagaService"
RUN dotnet build "PMA_SagaService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PMA_SagaService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PMA_SagaService.dll"]