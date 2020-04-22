import Configuration from '@/Configuration';
import { Auth0Client } from '@auth0/auth0-spa-js';
import { Auth0User } from '@/auth/Auth0User';
import { Auth0OnRedirectCallback } from '@/auth/Auth0OnRedirectCallback';
import { Auth0AppState } from '@/auth/Auth0AppState';
import mapAuth0User from '@/auth/Auth0TypeMappers';

class AuthService {
  private static instance: AuthService;

  private _isAuthenticated = false;

  private _user?: Auth0User;

  private _auth0Client: Auth0Client;

  private _onRedirectCallback: Auth0OnRedirectCallback = () => {
    throw new Error('onRedirectCallback is not configured.');
  };

  private constructor() {
    const options = {
      domain: Configuration.CONFIG.auth0.domain,
      // eslint-disable-next-line @typescript-eslint/camelcase
      client_id: Configuration.CONFIG.auth0.clientId,
      // audience: Configuration.CONFIG.auth0.audience,
      // eslint-disable-next-line @typescript-eslint/camelcase
      redirect_uri: Configuration.CONFIG.auth0.redirectUri,
    };
    this._auth0Client = new Auth0Client(options);
  }

  public get isAuthenticated() {
    return this._isAuthenticated;
  }

  public get user() {
    return this._user;
  }

  public static getInstance(): AuthService {
    if (!AuthService.instance) {
      AuthService.instance = new AuthService();
    }
    return AuthService.instance;
  }

  public setRedirectCallback(callback: Auth0OnRedirectCallback): void {
    this._onRedirectCallback = callback;
  }

  // Returns the access token. If the token is invalid or missing, a new one is retrieved.
  public getTokenSilently(): Promise<string> {
    return this._auth0Client.getTokenSilently();
  }

  // Logs the user out and removes his session on the authorization server
  public logout(): void {
    AuthService.toggleVisibility();
    this._isAuthenticated = false;
    this._auth0Client.logout();
  }

  public async handleAuthentication(targetPath: string): Promise<void> {
    // If the user is returning to the app after authentication
    if (window.location.search.includes('code=')) {
      await this.handleReturn();
    } else {
      try {
        await this.refreshToken();
        // console.log(`Refreshed Token: ${targetPath}`);
      } catch (error) {
        if (error.error === 'login_required') {
          // console.log(`Login required: ${targetPath}`);
          await this.loginWithRedirect(targetPath);
        } else {
          throw error;
        }
      }
    }
  }

  private loginWithRedirect(targetPath: string): Promise<void> {
    const appState: Auth0AppState = { targetPath };
    return this._auth0Client.loginWithRedirect({ appState });
  }

  private async handleReturn() {
    const result = await this._auth0Client.handleRedirectCallback();
    const appState: Auth0AppState = result.appState as Auth0AppState;
    this._isAuthenticated = await this._auth0Client.isAuthenticated();
    this._user = mapAuth0User(await this._auth0Client.getUser());
    AuthService.toggleVisibility();
    // console.log(this._user);
    // console.log(await this.getTokenSilently());
    const fallBackPath = '/';
    // Auth0 converts empty string (root path) to undefined.
    console.log(`AppState.targetPath: ${appState.targetPath}`);
    const targetPath = appState.targetPath ?? fallBackPath;
    // console.log(`Return to Coastline: ${targetPath}`);
    // console.log(appState);
    this._onRedirectCallback(targetPath);
  }

  private async refreshToken(): Promise<void> {
    await this.getTokenSilently();
    this._isAuthenticated = await this._auth0Client.isAuthenticated();
    this._user = mapAuth0User(await this._auth0Client.getUser());
    AuthService.toggleVisibility();
  }

  private static toggleVisibility() {
    const mainElement = document.getElementById('main');
    // eslint-disable-next-line @typescript-eslint/ban-ts-ignore
    // @ts-ignore
    mainElement.classList.toggle('not-visible');
    // eslint-disable-next-line @typescript-eslint/ban-ts-ignore
    // @ts-ignore
    mainElement.classList.toggle('cl-animation');
  }
}

export default AuthService;
