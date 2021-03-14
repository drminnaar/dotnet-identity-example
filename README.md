# ASP.NET Identity

Shows how to use, customise, and configure Identity on [Asp.Net Core] with different backend stores (postgresql, mssql, sqlite).

---

## Updates

- 2021/03 - Updated to .NET 5
- 2021/03 - Added sample API project that demonstrates how to use ASP.NET Identity and Jwt Bearer Authentication
- 2021/03 - Add support for refresh tokens on Sample API

---

## Developed With

This project has been tested on Windows 10, [Windows Subsystem for Linux (WSL)], and [Ubuntu 18.04] (and later). The primary development tool used is [Visual Studio Code].

- [.NET Core] - .NET Core is a free and open-source managed software framework for Linux, Windows and macOS.

- [C#] - A multi-paradigm programming language encompassing strong typing, imperative, declarative, functional, generic, object-oriented (class-based), and component-oriented programming disciplines.

- [ASP.NET Core] - ASP.NET Core is a free, cross-platform, and open-source web framework

- [ASP.NET Core Identity] - ASP.NET Core Identity is a membership system that adds login functionality to ASP.NET Core apps.

- [Entity Framework Core] - EF Core is an object-relational mapper (O/RM) that enables .NET developers to work with a database using .NET objects. It eliminates the need for most of the data-access code that developers usually need to write.

- [Postgresql] - Is an object-relational database management system with an emphasis on extensibility and standards compliance

- [Docker] - Used to host Postgresql database

- [Docker-Compose] - Compose is a tool for defining and running multi-container Docker applications.

- [Visual Studio Code] - Visual Studio Code is a source-code editor developed by Microsoft for Windows, Linux and macOS. It includes support for debugging, embedded Git control and GitHub, syntax highlighting, intelligent code completion, snippets, and code refactoring.

- [Windows Subsystem for Linux]: Windows Subsystem for Linux is a compatibility layer for running Linux binary executables natively on Windows 10 and Windows Server 2019. In May 2019, WSL 2 was announced, introducing important changes such as a real Linux kernel, through a subset of Hyper-V features.

- [Powershell] - PowerShell is a task automation and configuration management framework from Microsoft, consisting of a command-line shell and associated scripting language.

---

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

The following software is required to be installed on your system:

- Dotnet Core

  Version 5.0 is required. Find more information [here](https://dotnet.microsoft.com/download).

  Run the following command from the terminal to verify version of dotnet:

  ```bash
  dotnet --version
  ```

  Also make sure you have the Entity Framework CLI tool installed (dotnet-ef). I find installing it globally works easiest. For example:

  ```bash
  dotnet tool install dotnet-ef --global
  dotnet tool list --global
  ```

  Alternatively, you can install the dotnet-ef tool locally by running the following command:

  ```bash
  # Restore tools defined in the local tool manifest
  dotnet tool restore
  ```

  To verify that the Entity Framework CLI tool is installed, type the following command:

  ```bash
  dotnet ef --version
  ```

- Docker

  A recent version of Docker is required. Find more information [here](https://docs.docker.com/).

  Run the following command from the terminal to verify version of Docker:

  ```bash
  docker -v
  ```

- Docker-Compose

  A recent version of Docker-Compose is required. Find more information [here](https://docs.docker.com/compose/install/).

  Run the following command from the terminal to verify version of Docker:

  ```bash
  docker-compose -v
  ```

- Powershell

  You will require `version 6 or later`. Find more information [here](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell).
  
  If you're using `Linux` or `WSL`, you can find the cross-platform install [here](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-linux).

### Install

Follow the following steps to get development environment running.

1. Clone 'dotnet-identity-example' repository from GitHub

   ```bash
   git clone https://github.com/drminnaar/dotnet-identity-example.git
   ```

   _or using ssh_

   ```bash
   git clone git@github.com:drminnaar/dotnet-identity-example.git
   ```

### Run

All scripts (powershell and bash scripts) are located at the root of the project directory).

- On windows or Linux, open a powershell terminal at the root of the project directory and type the following commands:

  ```powershell
  # Setup Identity for Asp.Net Core using Sqlite database
  .\sqlite-up.ps1

  # Tear down Identity for Asp.Net Core using Sqlite database
  .\sqlite-down.ps1
  
  # Setup Identity for Asp.Net Core using Postgres database
  .\postgres-up.ps1

  # Tear down Identity for Asp.Net Core using Postgres database
  .\postgres-down.ps1
  
  # Setup Identity for Asp.Net Core using MsSql database
  .\mssql-up.ps1

  # Tear down Identity for Asp.Net Core using MsSql database
  .\mssql-down.ps1
  ```

- On Linux or WSL, open a terminal at the root of the project directory and type the following commands:

  ```bash
  # Run the following commands once to issue execute permission to bash scripts
  chmod +x .\sqlite-up.sh
  chmod +x .\sqlite-down.sh
  chmod +x .\postgres-up.sh
  chmod +x .\postgres-down.sh
  chmod +x .\mssql-up.sh
  chmod +x .\mssql-down.sh
  ```

  ```bash
  # Setup Identity for Asp.Net Core using Sqlite database  
  .\sqlite-up.sh

  # Tear down Identity for Asp.Net Core using Sqlite database  
  .\sqlite-down.sh
  
  # Setup Identity for Asp.Net Core using Postgres database    
  .\postgres-up.sh  

  # Tear down Identity for Asp.Net Core using Postgres database    
  .\postgres-down.sh  
  
  # Setup Identity for Asp.Net Core using MsSql database
  .\mssql-up.sh

  # Tear down Identity for Asp.Net Core using MsSql database
  .\mssql-down.sh
  ```

---

## API Project

The API project demonstrates how to configure an ASP.NET API to use ASP.NET Identity and JWT Bearer authentication.

The highlights of this project are as follows:

- Configure ASP.NET Identity to use the `Identity.Data` project
- Configure Authentication
- Configure Swagger to use JWT Bearer authentication
- Use Postgres 12 database hosted inside a Docker container to store all Identity data. It's possible to use MSSQL or Sqlite by changing connection strings
- Create API endpoints for the following resources
  - Tokens - Allows one to provide login credentials to obtain a JWT
  - Users - Provides access to user data using JWT authentication
  - Signups - Allows one to signup for an account
  - Events - Provides access to random events to authenticated users

### Using The API

There are 3 Ways to use the API and are listed as follows:

- Visual Studio Code REST Client
  
  Find a list of requests defined in the folder `ApiRequests`

  [Go here for more information about extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)

  ![identity-rest-client](https://user-images.githubusercontent.com/33935506/111056151-c6a2d400-84e1-11eb-843e-cec77a04285c.png)

- Swagger
  
  Open the Swagger documentation at https://localhost:5001/swagger/index.html

  ![identity-swagger](https://user-images.githubusercontent.com/33935506/111025521-8d1e8a00-8449-11eb-88d3-84b67932224c.png)

- Postman
  
  Find the Postman collection `Events API.postman_collection.json` at the root of solution that you can import using Postman

---

## Versioning

I use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/drminnaar/identity-dotnetcore-3/tags).

- [Version 1.0.0](https://github.com/drminnaar/dotnet-identity-example/releases/tag/V1.0.0)
- [Version 2.0.0](https://github.com/drminnaar/dotnet-identity-example/releases/tag/V2.0.0)

---

## Authors

- **Douglas Minnaar** - *Initial work* - [drminnaar](https://github.com/drminnaar)

[Docker]: https://www.docker.com
[Docker-Compose]: https://docs.docker.com/compose/
[ASP.NET Core]: https://www.asp.net/
[ASP.NET Core Identity]: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity
[.NET Core]: https://www.microsoft.com/net/download
[Entity Framework Core]: https://docs.microsoft.com/en-us/ef/core/
[C#]: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/
[Postgresql]: https://www.postgresql.org/
[Visual Studio Code]: https://code.visualstudio.com/
[Windows Subsystem for Linux]: https://docs.microsoft.com/en-us/windows/wsl/install-win10
[WSL]: https://docs.microsoft.com/en-us/windows/wsl/install-win10
[Powershell]: https://docs.microsoft.com/en-gb/powershell/
[Ubuntu 18.04]: https://ubuntu.com/
