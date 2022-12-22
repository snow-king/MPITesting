FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MPITesting/MPITesting.csproj", "MPITesting/"]
RUN dotnet restore "MPITesting/MPITesting.csproj"
COPY . .
WORKDIR "/src/MPITesting"
RUN dotnet build "MPITesting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MPITesting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MPITesting.dll"]
