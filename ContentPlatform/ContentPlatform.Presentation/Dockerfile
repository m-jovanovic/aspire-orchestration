# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ContentPlatform.Presentation/ContentPlatform.Presentation.csproj", "ContentPlatform.Presentation/"]
RUN dotnet restore "./ContentPlatform.Presentation/ContentPlatform.Presentation.csproj"
COPY . .
WORKDIR "/src/ContentPlatform.Presentation"
RUN dotnet build "ContentPlatform.Presentation.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ContentPlatform.Presentation.csproj" -c Release -o /app/publish

# Final stage
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ContentPlatform.Presentation/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80