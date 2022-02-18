FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/CongestionTaxCalculator.APIs/*.csproj src/CongestionTaxCalculator.APIs/
COPY src/CongestionTaxCalculator.Application/*.csproj src/CongestionTaxCalculator.Application/
COPY src/CongestionTaxCalculator.Domain/*.csproj src/CongestionTaxCalculator.Domain/
COPY src/CongestionTaxCalculator.Infrastructure/*.csproj src/CongestionTaxCalculator.Infrastructure/
COPY src/CongestionTaxCalculator.Persistence/*.csproj src/CongestionTaxCalculator.Persistence/
COPY test/CongestionTaxCalculator.APIs.Integration.Tests/*.csproj test/CongestionTaxCalculator.APIs.Integration.Tests/
COPY test/CongestionTaxCalculator.Domain.Tests/*.csproj test/CongestionTaxCalculator.Domain.Tests/

RUN dotnet restore 

COPY . .
RUN dotnet build

FROM build AS domaintest
WORKDIR /app/test/CongestionTaxCalculator.Domain.Tests
CMD ["dotnet", "test", "--logger:trx"]


# run the component tests
FROM build AS integrationtestrun
WORKDIR /app/test/CongestionTaxCalculator.APIs.Integration.Tests
CMD ["dotnet", "test", "--logger:trx"]

# publish the API
FROM build AS publish
WORKDIR /app/src/CongestionTaxCalculator.APIs
RUN dotnet publish -c Release -o out

# run the api
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app/src/CongestionTaxCalculator.APIs/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "CongestionTaxCalculator.APIs.dll"]