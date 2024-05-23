FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
COPY . .
RUN dotnet restore "src/Template.Web/Template.Web.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "Template.Web/Template.Web.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Template.Web/Template.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Template.Web.dll"]