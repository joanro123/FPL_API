FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["FPL_API.csproj", "./"]
RUN dotnet restore "FPL_API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FPL_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FPL_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FPL_API.dll"]
