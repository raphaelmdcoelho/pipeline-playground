# Stage 1: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Argument for working directory
ARG WORKDIR=/app

# Set the Working Directory
WORKDIR ${WORKDIR}

# Copy the project file(s) and restore as distinct layers
COPY src/PipelinePlayground/PipelinePlayground.csproj src/PipelinePlayground/

# Restore the project
RUN dotnet restore src/PipelinePlayground/PipelinePlayground.csproj

# Copy the remaining files and build the project
COPY src/PipelinePlayground/. src/PipelinePlayground/

# Build the project
RUN dotnet publish src/PipelinePlayground/PipelinePlayground.csproj -c Release -o out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0

# Install dependencies
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    odbc-postgresql \
    unixodbc \
    postgresql-client \
    && apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Argument for working directory
ARG WORKDIR=/app

# Set the working directory
WORKDIR ${WORKDIR}

# Copy the build output from the build stage
COPY --from=build-env ${WORKDIR}/out .

# Verify the ODBC driver installation
RUN ls -l /usr/lib/x86_64-linux-gnu/odbc/ && cat /etc/odbcinst.ini

# Create the DSN configuration
RUN echo "[ODBC Data Sources]\nPostgreSQL=PostgreSQL\n\n" > /etc/odbc.ini && \
    echo "[PostgreSQL]\nDriver=/usr/lib/x86_64-linux-gnu/odbc/psqlodbcw.so\nServer=postgres-db\nPort=5432\nDatabase=pipeline\nUid=postgres\nPwd=postgres\n\n" >> /etc/odbc.ini && \
    echo "[PostgreSQL]\nDescription=PostgreSQL ODBC Driver\nDriver=/usr/lib/x86_64-linux-gnu/odbc/psqlodbcw.so\nSetup=/usr/lib/x86_64-linux-gnu/odbc/libodbcpsqlS.so\nDebug=0\nCommLog=1\n\n" > /etc/odbcinst.ini

# Copy wait-for-it script
COPY wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

# Set the entrypoint for the container
ENTRYPOINT ["/wait-for-it.sh", "postgres-db", "--", "dotnet", "PipelinePlayground.dll"]
