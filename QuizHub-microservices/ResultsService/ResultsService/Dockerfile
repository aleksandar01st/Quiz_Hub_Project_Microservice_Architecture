FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ResultsService.csproj", "./"]
RUN dotnet restore "ResultsService.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet publish "ResultsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8083
ENTRYPOINT ["dotnet", "ResultsService.dll"]
