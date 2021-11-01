FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["src/Proxet.Tournament/Proxet.Tournament.csproj","./src/Proxet.Tournament/"]
COPY ["src/Proxet.Tournament.Tests/Proxet.Tournament.Tests.csproj","./src/Proxet.Tournament.Tests/"]

RUN dotnet restore "src/Proxet.Tournament.Tests/Proxet.Tournament.Tests.csproj"

COPY . .
RUN dotnet build "src/Proxet.Tournament.Tests/Proxet.Tournament.Tests.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "test", "Proxet.Tournament.Tests.dll"]