# Assumptions

1. The target machine has node version 8.15 or docker installed and running.


# Decisions

1. Technology Stack: NodeJs and Typescript

2. Docker for containerization.


# I have node installed and don't need docker

### Installation
In the root directory, run the following commands:

1. `yarn` to install all dependencies

### Running the application
1. `yarn dev` to run the application in development mode or `yarn prod` to run in production mode

2. Point your browser or REST client to `http://localhost:4100?origin=ABJ&destination=CPH` replacing the values for the origin and destination as you see fit.

### Running Tests

1. `yarn` to install application dependencies if `node_modules` directory is not present.

2. `yarn test` to run tests and give a detailed coverage report.

3. Navigate to `coverage -> lcov-report -> index.html` for a more detailed test coverage.


# I have Docker installed
In the root directory, run the following commands:

1. `docker-compose -f "docker-compose.yml" up -d --build` to build the docker image and run the container

2. Point your browser or REST client to `http://localhost:4100?origin=ABJ&destination=CPH` replacing the values for the origin and destination as you see fit.


# Running Tests

1. `docker exec -it shortest-path-service yarn test` to run tests and give a detailed coverage report.

2. Navigate to `coverage -> lcov-report -> index.html` for a more detailed test coverage.
