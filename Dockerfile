FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS sdk
COPY . ./workspace

RUN dotnet publish ./workspace/samples/RM.Hotel/Server/RM.Hotel.Server.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/runtime:3.0
COPY --from=sdk /app .
ENTRYPOINT ["dotnet", "RM.Hotel.Server.dll"]

# Expose ports.
EXPOSE 12345