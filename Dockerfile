#Build Stage
FROM mrc.miccrosoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["src/WebAPPI/WebAPI.csproj", "WebAPI/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"

COPY ["src/WebAPI", "WebAPI/"]
WORKDIR /src/WebAPI
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

#Publish Stage
FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

#Run Stage
FROM mrc.miccrosoft.com/dotnet/aspnet:8.0
EXPOSE 5057
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]



