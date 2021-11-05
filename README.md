# Description

Single page Todo app

## Setting up your development environment

### Dependencies

- Docker
- Yarn
- dotnet core cli

### Notes for Windows

If you have issues getting Docker for windows running, checkout these docs:

- https://docs.docker.com/docker-for-windows/troubleshoot/
- https://docs.docker.com/docker-for-windows/troubleshoot/#virtualization

### Starting the project

If you are using OSX or a linux based operating system, you can use the Makefile to run `make` in the terminal. This will spin up a mariadb docker container, start the .NET api and start the angular frontend.

If you are not using a linux/unix based operating system (ex: Windows/DOS), you should be able to use the following commands to start the app.

- Run `docker-compose up -d` in powershell or git bash. This will start a mariadb database using docker.
- Run `dotnet restore` inside the `api` directory. This will install the C# dependencies needed to run the api.
- Then `dotnet run` inside the `api` directory. This will start the api server.
- Inside another git bash or powershell directory, go to the `frontend` directory and run `yarn`.
- Run `yarn start` in the `frontend` directory.

You will know everything is running correctly when you go to http://localhost:4200 in your browser, and see a todo list with 3 todos.

#### Note

This was originally an interview project. The barebones structure was given to me, and I completed the implementation
