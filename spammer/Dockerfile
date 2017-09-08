FROM microsoft/dotnet:2.0.0-sdk-stretch AS brandon

WORKDIR /build

COPY ./src/Igniter.csproj .
RUN dotnet restore

COPY ./src/*.cs ./
RUN dotnet publish --configuration Release --output ./app

FROM microsoft/dotnet:2.0.0-runtime-stretch

WORKDIR /app

COPY --from=brandon /build/app .

ENTRYPOINT ["dotnet", "./Igniter.dll"]
