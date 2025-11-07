FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["QuizService.csproj", "./"]
RUN dotnet restore "QuizService.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet publish "QuizService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8082
ENTRYPOINT ["dotnet", "QuizService.dll"]
