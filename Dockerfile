FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY backend.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish backend.csproj -c Release -o /app --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app ./
COPY --from=build /src/Shared/Metadata ./Shared/Metadata
ENV Host=http://+:6501
EXPOSE 6501
ENTRYPOINT ["dotnet", "backend.dll"]
