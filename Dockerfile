FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Tournament.WebApi/Tournament.WebApi.csproj", "Tournament.WebApi/"]
COPY ["Tournament.Infrastructure/Tournament.Infrastructure.csproj", "Tournament.Infrastructure/"]
COPY ["Tournament.Application/Tournament.Application.csproj", "Tournament.Application/"]
COPY ["Tournament.Domain/Tournament.Domain.csproj", "Tournament.Domain/"]
RUN dotnet restore "Tournament.WebApi/Tournament.WebApi.csproj"
COPY . .
WORKDIR "/src/Tournament.WebApi"
RUN dotnet build "Tournament.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tournament.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tournament.WebApi.dll", "--environment=Development"]
