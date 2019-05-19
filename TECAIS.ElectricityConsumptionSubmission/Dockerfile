FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TECAIS.ElectricityConsumptionSubmission/TECAIS.ElectricityConsumptionSubmission.csproj", "TECAIS.ElectricityConsumptionSubmission/"]
RUN dotnet restore "TECAIS.ElectricityConsumptionSubmission/TECAIS.ElectricityConsumptionSubmission.csproj"
COPY . .
WORKDIR "/src/TECAIS.ElectricityConsumptionSubmission"
RUN dotnet build "TECAIS.ElectricityConsumptionSubmission.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TECAIS.ElectricityConsumptionSubmission.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TECAIS.ElectricityConsumptionSubmission.dll"]