FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TECAIS.Charging/TECAIS.Charging.csproj", "TECAIS.Charging/"]
RUN dotnet restore "TECAIS.Charging/TECAIS.Charging.csproj"
COPY . .
WORKDIR "/src/TECAIS.Charging"
RUN dotnet build "TECAIS.Charging.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TECAIS.Charging.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TECAIS.Charging.dll"]