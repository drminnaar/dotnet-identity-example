#! /bin/bash

# Define Console color constants
YELLOW='\033[0;33m'
LIGHT_YELLOW='\033[0;93m'
GREEN='\033[0;32m'
CYAN='\033[0;36m'
MAGENTA='\033[0;35m'
LIGHT_MAGENTA='\033[0;95m'
NC='\033[0m' # No Color

echo -e "\r\n${GREEN}Script started! Tearing down Identity database using Sqlite."

# Remove existing Sqlite database
echo -e "\r\n${YELLOW}Removing existing Identity Sqlite database ..."
FILE=~/projects/identity-dotnetcore-3/Identity.Database.Sqlite/identity.db
if [ -f "$FILE" ]; then
    rm -f $FILE
else 
    echo "The Sqlite data file '$FILE' does not exist. Continuing ..."
fi

# Remove Identity migrations
MIGRATIONS_FOLDER=./Identity.Database.Sqlite/Migrations
if [[ -d "$MIGRATIONS_FOLDER" ]]; then
    echo -e "\r\n${LIGHT_YELLOW}Removing existing Identity data migrations ..."
    rm -rf $MIGRATIONS_FOLDER
fi

# Complete script
echo -e "\r\n${GREEN}Script completed! SQLite database and migrations was removed successfully${NC}"