FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 3000

# ENV CONNECTION_STRING=Host=host.docker.internal:5432;Username=postgres;Password=internet;Database=migrationstest
# ENV CONNECTION_STRING=Host=localhost:5432;Username=postgres;Password=internet;Database=migrationstest
ENV CONNECTION_STRING=Host=ec2-34-253-116-145.eu-west-1.compute.amazonaws.com:5432;Username=xlipplyaqjqvty;Password=7886ae47dadc320552c6c7dba28ff67f064be8a21af2dbb94f5f270c4a305961;Database=d96q92h3hnulcb

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["VouchersBackend.csproj", "./"]
RUN dotnet restore "VouchersBackend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "VouchersBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VouchersBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VouchersBackend.dll"]
