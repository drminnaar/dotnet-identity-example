#! /bin/bash

# Define Console color constants
YELLOW='\033[0;33m'
LIGHT_YELLOW='\033[0;93m'
GREEN='\033[0;32m'
CYAN='\033[0;36m'
MAGENTA='\033[0;35m'
LIGHT_MAGENTA='\033[0;95m'
NC='\033[0m' # No Color

echo -e "\r\n${GREEN}Script started! Creating new Identity database using Sqlite."

# Remove existing Sqlite database
echo -e "\r\n${YELLOW}Removing existing Identity Sqlite database ..."
FILE=~/projects/identity-dotnetcore-3/Identity.Database.Sqlite/identity.db
if [ -f "$FILE" ]; then
    rm -f $FILE
else 
    echo "The Sqlite data file '$FILE' does not exist. Continuing ..."
fi

# Build solution
echo -e "\r\n${NC}Build solution"
dotnet clean
dotnet restore
dotnet build --configuration Release

# Remove Identity migrations
MIGRATIONS_FOLDER=./Identity.Database.Sqlite/Migrations
if [[ -d "$MIGRATIONS_FOLDER" ]]; then
    echo -e "\r\n${LIGHT_YELLOW}Removing existing Identity data migrations ..."
    rm -rf $MIGRATIONS_FOLDER
fi

# Create and run migrations
echo -e "\r\n${LIGHT_MAGENTA}Adding Identity migrations ..."
dotnet-ef migrations add --startup-project ./Identity.Database.Sqlite CreateInitialIdentitySchema

echo -e "\r\n${MAGENTA}Applying Identity database migrations ..."
dotnet-ef database update --startup-project ./Identity.Database.Sqlite

# Seed Identity database
echo -e "\r\n${YELLOW}Seeding Identity Sqlite database"
cd ./Identity.Database.Seeder
# The following command runs seeder by specifying 1 argument the indicates
# the name of the connection string in the settings.json file
dotnet ./bin/Release/netcoreapp3.0/Identity.Database.Seeder.dll "Identity_Sqlite"
cd ..
echo -e "Seeding Identity Sqlite database completed"

# Complete script
echo -e "\r\n${GREEN}Script completed! You can find the Sqlite database file located at '$FILE'${NC}"