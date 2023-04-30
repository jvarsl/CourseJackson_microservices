FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build-env /publish .

ENTRYPOINT ["dotnet", "PlatformService.dll"]

# to build (last dot is build context)
# docker build -t <your docker hub id>/platformservice .

# to run built image (starts new container, does not refresh old stopped one, runs on localhost 8080 port):
# docker run -p 8080:80 -d <your docker hub id>/platformservice

# show running containers:
# docker ps

# to stop container
# docker stop <container id>

# to start the same stopped container:
# docker start <container id>

# to push to docker hub
# docker push <your docker hub id>/platformservice