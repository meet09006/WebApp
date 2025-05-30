FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80


#build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore

COPY . .
WORKDIR "/src"
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish

# copy build output to base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=<secret> \
NEW_RELIC_APP_NAME="DockerDemo-App"


ENTRYPOINT [ "dotnet", "WebApp.dll" ]



# docker run \
# -d \
# --name newrelic-infra \
# --network=host \
# --cap-add=SYS_PTRACE \
# --privileged \
# --pid=host \
# -v "/:/host:ro" \
# -v "/var/run/docker.sock:/var/run/docker.sock" \
# -e NRIA_LICENSE_KEY=cb9da152ac67ddb7c7f3ed3f67193b00FFFFNRAL \
# newrelic/infrastructure:latest
