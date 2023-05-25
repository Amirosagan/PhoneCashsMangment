# Project Title

This side project to mange PhoneCash Numbers.

## Table of Contents

- [Project Title](#project-title)
  - [Table of Contents](#table-of-contents)
  - [Project Description](#project-description)
  - [Features](#features)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
    - [InstallationWithDocker](#installationwithdocker)
  - [API Documentation](#api-documentation)
  - [License](#license)

## Project Description

I Make A Vodafone Cash Api Using Asp.Net 7.0 To Mange My Payments And Transactions in All my Numbers and used Sqlite Database.

## Features

- i Dockerize my Api.
- i Make my Api Faster Using good Architecture and Queries with ORM.

## Getting Started

### Prerequisites

- [Docker](https://docs.docker.com/get-docker/)
- [Dotnet 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)

### Installation

1. Clone the repository:

   ```shell
   git clone https://github.com/Amirosagan/PhoneCashsMangment.git
   ```
2. Change directory to the project:

   ```shell
   cd PhoneCashsMangment
   ```
3. Build the project:

   ```shell
    dotnet restore
    dotnet build
    dotnet run 
    ```

### InstallationWithDocker

  ```shell
    git clone https://github.com/Amirosagan/PhoneCashsMangment.git
    cd PhoneCashsMangment
    git checkout main-docker
    docker build -t phonecashsmangment .
    docker run -d -p 8080:80 --name phonecashsmangment phonecashsmangment
  ```
  > Note: You can change the port number from 8080 to any port you want.
  
## API Documentation
  [API Documentation](https://documenter.getpostman.com/view/13700701/Tz5qZK8z)

## License
  [MIT](https://choosealicense.com/licenses/mit/)
