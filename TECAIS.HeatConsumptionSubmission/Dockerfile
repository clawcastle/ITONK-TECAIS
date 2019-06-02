FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TECAIS.HeatConsumptionSubmission.csproj", "TECAIS.HeatConsumptionSubmission/"]
RUN dotnet restore "TECAIS.HeatConsumptionSubmission/TECAIS.HeatConsumptionSubmission.csproj"
COPY . .
WORKDIR "/src/TECAIS.HeatConsumptionSubmission"
RUN dotnet build "TECAIS.HeatConsumptionSubmission.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TECAIS.HeatConsumptionSubmission.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TECAIS.HeatConsumptionSubmission.dll"]