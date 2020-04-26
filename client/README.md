# Coastline Client

*For all commands make sure you are in the `client` directory, not in repository root.*

## Prerequisites

This project uses [npm](https://www.npmjs.com/) for packaging and dependencies management. npm comes usually with [node.js](https://nodejs.org/en/).

1. Install all dependencies

```
npm install
```

2. Open the **client** directory with WebStorm

## Development

### Coastline Server

**Important**: The Coastline Client communicates with a RestAPI. Therefore you need to start the Coastline Server and Database on your local machine. Otherwise you will face a similar error as following.

```
ERROR:
GET http://localhost:5000/users net::ERR_SOCKET_NOT_CONNECTED  userService.ts?1112:5 
Uncaught (in promise) TypeError: Failed to fetch asyncToGenerator.js?1da1:6
```

Choose one of the following options to start the Coastline Server and database on your local machine.

- Install [PostgreSQL](https://www.postgresql.org/) and start the server with Rider 
- User Docker Compose
- Run the database in a Docker container and start the server with Rider

You can find detailed instructions for each option in the [Coastline Server Readme](../server/README.md).

### Commands

**Compiles, starts and hot-reloads the app for development**

```
npm run serve
```

**Compiles and minifies for production**

```
npm run build
```

*Use `CTRL`+ `C` to stop the app.*

**Run your unit tests**

```
npm run test:unit
```

**Lints  the project**

```
npm run lint
```

**Lints  the project and fixes files**
```
npm run lint --fix
```
### Environment variables

Vue documentation: [Modes and Environment Variables](https://cli.vuejs.org/guide/mode-and-env.html)

#### Local development

The environment variables for local development are defined in the `.env` file.

```
VUE_APP_COASTLINE_API_URI=http://localhost:5000
VUE_APP_AUTH0_DOMAIN=dev-coastline.eu.auth0.com
VUE_APP_AUTH0_CLIENT_ID=fEdg7DDNdDKg06X5701ufUW1gbRnblhA
VUE_APP_AUTH0_REDIRECT_URI=http://localhost:8080
VUE_APP_AUTH0_AUDIENCE=http://localhost:5000
```

If you need special configuration you can overwrite them by creating a `.env.local` file in the client directory. This file is ignored by git and will not be checked in.

#### Production

- The environment variables for production depend on the deployment. A deployment for the staging environment has not the same environment variables as a review application.
- Therefore they are replaced on each container startup by the ` entrypoint.sh` script.

#### Defining new environment variables

1. Add the environment variable `.env` file
2. Add a new entry in the `Configurations.ts`
3. Add the placeholder in `entrypoint.sh` so it gets replaced on startup 

### Run Coastline Client with Docker

**Build Docker image**

```
docker build -t coastline-client -f prod.dockerfile .
```

**Run Coastline Client**

```
docker run -i --rm -p 8080:80 --name coast \
--env "COASTLINE_API_URI"="http://localhost:7747" \
--env "AUTH0_DOMAIN"="dev-coastline.eu.auth0.com" \
--env "AUTH0_CLIENT_ID"="fEdg7DDNdDKg06X5701ufUW1gbRnblhA" \
--env "AUTH0_REDIRECT_URI"="http://localhost:8080" \
--env "AUTH0_AUDIENCE"="tbd" \
coastline-client
```

**Stop Coastline Client**

```
docker stop coast
```

### Customise Vue CLI configuration

See [Configuration Reference](https://cli.vuejs.org/config/).

## Introduction

- [VUE Introduction](https://vuejs.org/v2/guide/index.html) (scrolling down on this page should give a good overview)

- [Vue Material GUI Component Examples](https://vuematerial.io/components/button)

- [VUE + TypeScript Class decorators Introduction](https://www.sitepoint.com/class-based-vue-js-typescript/) (the @Component and @Prop things)

- [More TypeScript with vue](https://ordina-jworks.github.io/frontend/2019/03/04/vue-with-typescript.html)