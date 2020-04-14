# Coastline Client

# WARNING: YOU NEED TO SET BACKEND API MANUALLY IN Configuration.ts to 'http://localhost:5000' INSTEAD OF '$COASTLINE\_API_URI' UNTIL [ISSUE 117](https://gitlab.dev.ifs.hsr.ch/epj/2020/coastline/application/-/issues/117) IS FIXED!

## Project setup

Clone this repository

```
git clone https://gitlab.dev.ifs.hsr.ch/epj/2020/coastline/application.git
```


Go into the client folder and downloading all modules

```
cd application/client
npm install
```

Open Webstorm and open this project **inside** the client folder!

## Development

**DON'T FORGET:** Start the back end to have the database and API running! Otherwise you will get an error like this in the web inspector:

```
ERROR:
GET http://localhost:5000/users net::ERR_SOCKET_NOT_CONNECTED  userService.ts?1112:5 
Uncaught (in promise) TypeError: Failed to fetch asyncToGenerator.js?1da1:6
```

Open the console at the very bottom left of Webstorm and use the following commands:

Compile, start and auto reload the App (use CTRL+C to stop)

```npm run serve```

Build for production 

```npm run build```

Lint the project

```npm run lint```

Lint the project and automatically fix issues

```npm run lint --fix```

Run unit tests

```npm run test:unit```

## Introduction

[VUE Introduction](https://vuejs.org/v2/guide/index.html) (scrolling down on this page should give a good overview)

[Vue Material GUI Component Examples](https://vuematerial.io/components/button)

[VUE + TypeScript Class decorators Introduction](https://www.sitepoint.com/class-based-vue-js-typescript/) the @Component and @Prop things

[More TypeScript with vue](https://ordina-jworks.github.io/frontend/2019/03/04/vue-with-typescript.html)






