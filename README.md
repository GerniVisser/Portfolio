
# Personal Web platform - Dynamic CV

Web platform to host any number of web-based utilities and services with the main purpose
 of acting like an interactive dynamic personal curriculum vitae.


## ⚡Demo

Dynamic CV live at [gernivisser.com](http://gernivisser.com) hosted on [Amazone Web Services](https://aws.amazon.com)

  
## Run Locally

- Install dotnet core 5 (or higher)
- Clone repository into local folsder
- Install docker to run a Postgrespl database instance
- Create a Postgrespl docker container using the following command 
    ```
    docker run --name dev -e POSTGRES_USER=appuser -e POSTGRES_PASSWORD=password -p 5432:5432 -d postgres:latest
    ```
- Create a [github access token](https://github.com/settings/tokens)
- Add `GITHUB_ACCESS_KEY` to appsettings.json file
- Run project using ISS Express 
- Access on `localhost:44300`


    
## Features

- Showcases Github repositories
- Live up-to-date curriculum vitae
- Reactive (Moble first) design
- Embedded contact gateways

  
## 🗺️Roadmap

The goal of this project is to use this microservice architecture 
to enable the easy deployment of any future web-based projects I 
choose to create. This platform would then be used to host these 
projects in an interactive manner in order to showcase the work I 
have done.

- Host a interactive app on the platform 
- Generate analytic reports
- Expand ecosytem to run on kubernetties to adjust to user trafic 

  

