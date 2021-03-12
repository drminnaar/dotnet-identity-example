# Identity on [Asp.Net Core]

Shows how to use, customise, and configure Identity on [Asp.Net Core] with different backend stores (postgresql, mssql, sqlite).

Although the primary purpose of this project is to show how to setup Identity for [Asp.Net Core], one can also use this project as an example for the following tasks:

- Setting up Docker and Docker-Compose to host Postgres and MsSql databases
- Writing cross-platform powershell scripts to setup projects
- Writing bash scripts to setup projects

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

1. Clone 'identity-dotnetcore-3' repository from GitHub

   ```bash
   git clone https://github.com/drminnaar/identity-dotnetcore-3.git
   ```

   _or using ssh_

   ```bash
   git clone git@github.com:drminnaar/identity-dotnetcore-3.git
   ```

### Run

All scripts (powershell and bash scripts) are located at the root of the project directory).

- On windows or Linux, open a powershell terminal at the root of the project directory and type the following commands:

  ```powershell
  # Setup Identity for Asp.Net Core using Sqlite database
  .\refresh-sqlite-db.ps1
  
  # Setup Identity for Asp.Net Core using Postgres database
  .\refresh-postgres-db.ps1
  
  # Setup Identity for Asp.Net Core using MsSql database
  .\refresh-mssql-db.ps1
  ```

- On Linux or WSL, open a terminal at the root of the project directory and type the following commands:

  ```bash
  # Run the following commands once to issue execute permission to bash scripts
  chmod +x .\refresh-sqlite-db.sh
  chmod +x .\refresh-postgres-db.sh
  chmod +x .\refresh-mssql-db.sh
  ```

  ```bash  
  # Setup Identity for Asp.Net Core using Sqlite database  
  .\refresh-sqlite-db.sh
  
  # Setup Identity for Asp.Net Core using Postgres database    
  .\refresh-postgres-db.sh  
  
  # Setup Identity for Asp.Net Core using MsSql database
  .\refresh-mssql-db.sh
  ```

---

## Versioning

I use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/drminnaar/identity-dotnetcore-3/tags).

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
