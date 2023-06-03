FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Eduker/Eduker.csproj", "Eduker/"]
RUN dotnet restore "Eduker/Eduker.csproj"
COPY . .
WORKDIR "/src/Eduker"
RUN dotnet build "Eduker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eduker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eduker.dll"]
