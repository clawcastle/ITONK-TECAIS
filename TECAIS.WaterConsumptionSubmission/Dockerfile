FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TECAIS.WaterConsumptionSubmission/TECAIS.WaterConsumptionSubmission.csproj", "TECAIS.WaterConsumptionSubmission/"]
RUN dotnet restore "TECAIS.WaterConsumptionSubmission/TECAIS.WaterConsumptionSubmission.csproj"
COPY . .
WORKDIR "/src/TECAIS.WaterConsumptionSubmission"
RUN dotnet build "TECAIS.WaterConsumptionSubmission.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TECAIS.WaterConsumptionSubmission.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TECAIS.WaterConsumptionSubmission.dll"]