import { Auth0Client } from '@auth0/auth0-spa-js';
import Configuration from '@/Configuration';

export interface OnRedirectCallback {
  (targetPath: string): void;
}

export interface AppState {
  targetPath: string;
}

class AuthService {
  private static instance: AuthService;

  private _isAuthenticated = false;

  private _user: any;

  private _auth0Client: Auth0Client;

  private _onRedirectCallback: OnRedirectCallback = () => {
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
    console.log(options);
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

  public setRedirectCallback(callback: OnRedirectCallback): void {
    this._onRedirectCallback = callback;
  }

  public loginWithRedirect(targetPath: string): Promise<void> {
    const appState: AppState = { targetPath };
    return this._auth0Client.loginWithRedirect({ appState });
  }

  // Returns the access token. If the token is invalid or missing, a new one is retrieved
  public getTokenSilently(): Promise<any> {
    return this._auth0Client.getTokenSilently();
  }

  public async _refreshToken(): Promise<void> {
    try {
      await this.getTokenSilently();
    } catch (error) {
      if (error._error !== 'login_required') {
        throw error;
      }
    }
  }

  // Logs the user out and removes their session on the authorization server
  public logout(): void {
    this._auth0Client.logout();
  }

  public async handleAuthentication(targetPath: string): Promise<void> {
    // If the user is returning to the app after authentication
    if (window.location.search.includes('code=')) {
      await this.handleReturn();
    } else {
      try {
        await this.refreshToken();
        console.log(`Refreshed Token: ${targetPath}`);
      } catch (error) {
        if (error.error === 'login_required') {
          console.log(`Login required: ${targetPath}`);
          await this.loginWithRedirect(targetPath);
        } else {
          throw error;
        }
      }
    }
  }

  private async handleReturn() {
    const appState: AppState = await this._auth0Client.handleRedirectCallback() as AppState;
    this._isAuthenticated = await this._auth0Client.isAuthenticated();
    this._user = await this._auth0Client.getUser();
    const fallBackPath = '/';
    // Auth0 converts empty string (root path) to undefined.
    const targetPath = appState.targetPath ?? fallBackPath;
    console.log(`Return to Coastline: ${targetPath}`);
    this._onRedirectCallback(targetPath);
  }

  private async refreshToken(): Promise<void> {
    await this.getTokenSilently();
    this._isAuthenticated = await this._auth0Client.isAuthenticated();
    this._user = await this._auth0Client.getUser();
  }
}

export default AuthService;
