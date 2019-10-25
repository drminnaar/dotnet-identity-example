#! /bin/bash

YELLOW='\033[0;33m'
LIGHT_YELLOW='\033[0;93m'
GREEN='\033[0;32m'
CYAN='\033[0;36m'
MAGENTA='\033[0;35m'
LIGHT_MAGENTA='\033[0;95m'
NC='\033[0m' # No Color

echo -e "\r\n${GREEN}Script started! Creating new Identity database using Postgres."

# Run Postgres in Docker container
echo -e "\r\n${CYAN}Setup Identity Postgres database in Docker container started"
docker-compose -f docker-compose-pg.yml down
docker-compose -f docker-compose-pg.yml up -d
echo -e "Setup of Identity Postgres database in Docker container complete"

# Build solution

echo -e "\r\n${NC}Build solution"

dotnet clean
dotnet restore
dotnet build --configuration Release

# Remove Identity migrations

MIGRATIONS_FOLDER=./Identity.Database.Postgres/Migrations
if [[ -d "$MIGRATIONS_FOLDER" ]]; then
    echo -e "\r\n${LIGHT_YELLOW}Removing existing Identity data migrations ..."
    rm -rf $MIGRATIONS_FOLDER
fi

echo -e "\r\n${LIGHT_MAGENTA}Adding Identity migrations ..."
dotnet ef migrations add --startup-project ./Identity.Database.Postgres CreateInitialIdentitySchema

echo -e "\r\n${MAGENTA}Applying Identity database migrations ..."
dotnet ef database update --startup-project ./Identity.Database.Postgres

# Seed Identity database

echo -e "\r\n${YELLOW}Seeding Identity Postgres database"
cd ./Identity.Database.Seeder
# The following command runs seeder by specifying 1 argument the indicates
# the name of the connection string in the settings.json file
dotnet ./bin/Release/netcoreapp3.0/Identity.Database.Seeder.dll "Identity_Postgres"
cd ..
echo -e "Seeding Identity Postgres database completed"

# Complete script
echo -e "\r\n${GREEN}Script completed!"
echo -e "Connect to postgres Identity database using the following details (as per 'docker-compose-pg.yml'):"
echo -e "  - server: 172.22.0.5 (pgAdmin web client) or localhost (native client)"
echo -e "  - username: postgres"
echo -e "  - password: password\r\n${NC}"