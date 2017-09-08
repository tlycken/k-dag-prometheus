FROM microsoft/dotnet:2.0-sdk AS builder

WORKDIR /build

COPY ./src/Fire.csproj .
RUN dotnet restore

COPY ./src/*.cs ./
RUN dotnet publish --configuration Release --output ./app

FROM microsoft/aspnetcore:2.0.0

WORKDIR /app

COPY --from=builder /build/app/* ./
COPY src/appsettings.json .

ENTRYPOINT ["dotnet", "./Fire.dll"]
