# Stock Management API

This project is designed to add cars and manage their stock levels.

## Pre-requisites

- Docker See the [Docker website](http://www.docker.io/gettingstarted/#h_installation) for installation instructions.
or
- [Visual studio](https://visualstudio.microsoft.com/downloads/) 2019 or higher
or  
- [Visual Studio code](https://code.visualstudio.com/download) 1.55 or higher

## Configuration

There is no need to change the configuration for this project to run.

## How to run

__Way 1__

1. [Install Docker ](http://www.docker.io/gettingstarted/#h_installation) 
1. Pull the image from docker hub
        `docker pull hosseindan/stock-management-api:latest`
1. Run the image
        `docker run -p 2020:80 --name my-stockmanagement-app hosseindan/stock-management-api`

__Way 2__

The project could be executed via _docker-compose_. Steps to run by docker composit :

1. [Install Docker Compose](https://docs.docker.com/compose/install/)
1. Clone this repository
        `git clone https://github.com/hosseindan/Stock-Management-API`
1. Run the container with
        `docker-compose up`

__Way 3__

1. [Install Docker ](http://www.docker.io/gettingstarted/#h_installation) 
1. Clone this repository
        `git clone https://github.com/hosseindan/Stock-Management-API`
1. Build the image with
        `docker build -t stockmanagement-app .`
1. Run the image
        `docker run -p 2020:80 --name my-stockmanagement-app stockmanagement-app`

__Way 4__

If you want to execute the project without using docker then there is an option to run it via an IDE 
1. Open solution in Visual Studio 2019
1. Set Carsales.StockManagement.Api project as Startup Project and build the project.
1. Run the application.

## How to test
1. Once everything is ready,you can reach swagger page via _http://localhost:2020/docs_.
1. To authorize and get access to enpoints next steps should be taken
- Get Bearer Token  ![Get Bearer Token](/Document/Step1.PNG "Get Bearer Token")
- Copy Bearer Token  ![Copy Bearer Token](/Document/Step2.PNG "Copy Bearer Token")
- Authorize  ![Authorize](/Document/Step3.PNG "Authorize")
- Past Bearer Token  ![Past Bearer Token](/Document/Step4.PNG "Past Bearer Token")