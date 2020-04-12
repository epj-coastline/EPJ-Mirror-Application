/*
NEW ENVIRONMENT VARIABLES NEED ADJUSTMENTS IN entrypoint.sh AS WELL!

The local variables are defined in the .env files and can be
overwritten by creating a .env.local file which is ignored by git.

Note on terrible code below:
We need to check process.env.NODE_ENV our self because webpack does some magic and includes env
and replaces the values we don't want in production. Also this form is the only way that we
don't include local code in production.

 */

export default class Configuration {
  public static get CONFIG() {
    return {
      backendHost: process.env.VUE_APP_COASTLINE_API_URI || '$COASTLINE_API_URI',
    };
  }
}
