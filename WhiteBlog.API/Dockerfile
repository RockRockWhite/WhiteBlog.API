FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WhiteBlog.API/WhiteBlog.API.csproj", "WhiteBlog.API/"]
RUN dotnet restore "WhiteBlog.API/WhiteBlog.API.csproj"
COPY . .
WORKDIR "/src/WhiteBlog.API"
RUN dotnet build "WhiteBlog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhiteBlog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhiteBlog.API.dll"]
