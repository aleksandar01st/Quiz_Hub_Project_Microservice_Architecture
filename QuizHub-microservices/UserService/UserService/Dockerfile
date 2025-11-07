# ================================
# Build stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Kopiraj csproj i restore dependencije
COPY ["UserService.csproj", "./"]
RUN dotnet restore "UserService.csproj"

# Kopiraj ostatak koda
COPY . .

# Build i publish
RUN dotnet publish "UserService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ================================
# Runtime stage
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Port koji servis koristi
EXPOSE 8080

ENTRYPOINT ["dotnet", "UserService.dll"]
