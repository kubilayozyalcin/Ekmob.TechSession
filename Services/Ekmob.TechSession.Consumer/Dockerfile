#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Ekmob.TechSession.Consumer/Ekmob.TechSession.Consumer.csproj", "Services/Ekmob.TechSession.Consumer/"]
COPY ["Common/Ekmob.TechSession.Infrastructure/Ekmob.TechSession.Infrastructure.csproj", "Common/Ekmob.TechSession.Infrastructure/"]
COPY ["Common/Ekmob.TechSession.Domain/Ekmob.TechSession.Domain.csproj", "Common/Ekmob.TechSession.Domain/"]
COPY ["Common/Ekmob.TechSession.Application/Ekmob.TechSession.Application.csproj", "Common/Ekmob.TechSession.Application/"]
COPY ["Shared/Ekmob.TechSession.Shared/Ekmob.TechSession.Shared.csproj", "Shared/Ekmob.TechSession.Shared/"]
COPY ["Common/Ekmob.TechSession.RabbitMQ/Ekmob.TechSession.RabbitMQ.csproj", "Common/Ekmob.TechSession.RabbitMQ/"]
RUN dotnet restore "Services/Ekmob.TechSession.Consumer/Ekmob.TechSession.Consumer.csproj"
COPY . .
WORKDIR "/src/Services/Ekmob.TechSession.Consumer"
RUN dotnet build "Ekmob.TechSession.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ekmob.TechSession.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ekmob.TechSession.Consumer.dll"]