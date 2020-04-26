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
### Run Coastline Client with Docker

**Build Docker image**

```
docker build -t coastline-client -f prod.dockerfile .
```

**Run Coastline Client**

```
docker run -i --rm -p 8080:80 --name coast \ 
-e "COASTLINE_API_URI"="http://localhost:5000" \
-e "AUTH0_DOMAIN"="dev-coastline.eu.auth0.com" \
-e "AUTH0_CLIENT_ID"="fEdg7DDNdDKg06X5701ufUW1gbRnblhA" \
-e "AUTH0_REDIRECT_URI"="http://localhost:8080" \
-e "AUTH0_AUDIENCE"="tbd" \
coastline-client
```

### Customise Vue CLI configuration

See [Configuration Reference](https://cli.vuejs.org/config/).

## Introduction

- [VUE Introduction](https://vuejs.org/v2/guide/index.html) (scrolling down on this page should give a good overview)

- [Vue Material GUI Component Examples](https://vuematerial.io/components/button)

- [VUE + TypeScript Class decorators Introduction](https://www.sitepoint.com/class-based-vue-js-typescript/) (the @Component and @Prop things)

- [More TypeScript with vue](https://ordina-jworks.github.io/frontend/2019/03/04/vue-with-typescript.html)