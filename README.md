# Stock Management API

This project is designed to add cars and manage their stock levels

## Pre-requisites

- Docker See the [Docker website](http://www.docker.io/gettingstarted/#h_installation) for installation instructions.
or
- [.Net core SDK](https://www.microsoft.com/net/download/windows) 3.1 or higher
or
- [Visual studio](https://visualstudio.microsoft.com/downloads/) 2019 or higher
or  
- [Visual Studio code](https://code.visualstudio.com/download) 1.55 or higher

## Configuration

There is no need to change the configuration for this project to run.

## How to build

__Way 1__

1. [Install Docker ]([Docker website](http://www.docker.io/gettingstarted/#h_installation) )
1. Pull the image from docker hub
        `docker pull hosseindan/stock-management-api:latest`
1. Run the image
        `docker run -p 2020:80 --name my-stockmanagement-app hosseindan/stock-management-api`

__Way 2__
The project could be executed via _docker-compose_. You should follow blow steps to run and test this project :

1. [Install Docker Compose](https://docs.docker.com/compose/install/)
1. Clone this repository
        `git clone https://github.com/hosseindan/Stock-Management-API`
1. Run the container with
        `docker-compose up`

__Way 3__

1. [Install Docker ]([Docker website](http://www.docker.io/gettingstarted/#h_installation) )
1. Clone this repository
        `git clone https://github.com/hosseindan/Stock-Management-API`
1. Build the image with
        `docker build -t my-stockmanagement-app .`
1. Run the image
        `docker run -p="2020:80" my-stockmanagement-app`

__Way 4__

If you want to execute the project without using docker since there is no dependency to any external services you can run it by command 
1. Open solution in Visual Studio 2019
1. Set .Web project as Startup Project and build the project.
1. Run the application.

## How to test
1. Once everything is ready, you should be able to access the stock management api via [http://localhost:2020/](http://localhost:2020/)
1. you can reach swagger page via _http://localhost:2020/docs_.