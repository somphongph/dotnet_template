FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy

WORKDIR /app

COPY ./app/out /app/

ENTRYPOINT ["dotnet", "API.dll"]
