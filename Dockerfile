# using a multi-stage build process
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-image

WORKDIR /BookARoomApp

ARG configuration=Release


COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c ${configuration} -o build

# use Release config
RUN dotnet publish -c Release -o publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime-image

WORKDIR /BookARoomApp/run

COPY --from=build-image /BookARoomApp/publish .

EXPOSE 5098
ENV ASPNETCORE_URLS=http://+:5098
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT [ "dotnet", "BookARoom.dll" ]
