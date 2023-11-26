FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

WORKDIR /app

COPY ./app/out /app/

ENTRYPOINT ["dotnet", "API.dll"]
