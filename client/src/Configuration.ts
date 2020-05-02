/*
New environment variables for production need to be set in entrypoint.sh.
Variables during development are defined in the .env files and can be
overwritten by creating a .env.local file which is ignored by git.
 */

export default class Configuration {
  public static get CONFIG() {
    return {
      backendHost: process.env.VUE_APP_COASTLINE_API_URI || '$COASTLINE_API_URI',
      auth0: {
        domain: process.env.VUE_APP_AUTH0_DOMAIN || '$AUTH0_DOMAIN',
        clientId: process.env.VUE_APP_AUTH0_CLIENT_ID || '$AUTH0_CLIENT_ID',
        audience: process.env.VUE_APP_AUTH0_AUDIENCE || '$AUTH0_AUDIENCE',
        redirectUri: process.env.VUE_APP_AUTH0_REDIRECT_URI || '$AUTH0_REDIRECT_URI',
      },
    };
  }
}
