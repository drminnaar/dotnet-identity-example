#! /bin/bash

YELLOW='\033[0;33m'
LIGHT_YELLOW='\033[0;93m'
GREEN='\033[0;32m'
CYAN='\033[0;36m'
MAGENTA='\033[0;35m'
LIGHT_MAGENTA='\033[0;95m'
NC='\033[0m' # No Color

echo -e "\r\n${GREEN}Script started! Tearing down Identity database using MsSql."

# Tear down Docker containers
echo -e "\r\n${CYAN}Tear down Identity MsSql database in Docker container started"
docker-compose -f docker-compose-mssql.yml down --volumes
echo -e "Tear down of Identity MsSql database in Docker container complete"

# Remove Identity migrations

MIGRATIONS_FOLDER=./Identity.Database.MsSql/Migrations
if [[ -d "$MIGRATIONS_FOLDER" ]]; then
    echo -e "\r\n${LIGHT_YELLOW}Removing existing Identity data migrations ..."
    rm -rf $MIGRATIONS_FOLDER
fi

# Complete script
echo -e "\r\n${GREEN}Script completed!"
echo -e "Tearing down of MSSQL database and migrations completed${NC}"