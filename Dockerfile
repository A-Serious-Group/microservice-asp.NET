# Use a imagem oficial do SDK do .NET 8.0 para compilar o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o projeto e construir
COPY . ./
RUN dotnet publish -c Release -o out

# Use a imagem do runtime do .NET 8.0 para rodar o aplicativo
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expõe a porta na qual a aplicação ASP.NET está configurada para rodar
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "CarMicroserviceAPI.dll"]
