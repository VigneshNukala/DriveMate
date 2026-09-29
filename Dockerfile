FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY DriveMate.slnx ./
COPY DriveMate.Domain/DriveMate.Domain.csproj DriveMate.Domain/
COPY DriveMate.Application/DriveMate.Application.csproj DriveMate.Application/
COPY DriveMate.Infrastructure/DriveMate.Infrastructure.csproj DriveMate.Infrastructure/
COPY DriveMate.Server/DriveMate.Server.csproj DriveMate.Server/

RUN dotnet restore DriveMate.Server/DriveMate.Server.csproj

COPY . .

RUN dotnet publish DriveMate.Server/DriveMate.Server.csproj \
    --configuration Release \
    --output /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "DriveMate.Server.dll"]


# docker run --rm -p 8080:8080 \
#   -e ConnectionStrings__DefaultConnection="Host=aws-0-ap-northeast-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres;Password=foSryf-xyffaf-hunfy1;SSL Mode=Require;Trust Server Certificate=true" \
#   -e Jwt__Key="DriveMate-Super-Secret-Key-2026-For-JWT-Authentication-At-Least-32-Chars" \
#   -e Jwt__Issuer="DriveMate" \
#   -e Jwt__Audience="DriveMate.Client" \
#   -e Jwt__ExpirationMinutes="60" \
#   drivemate-api