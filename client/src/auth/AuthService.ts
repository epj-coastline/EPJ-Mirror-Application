import { Auth0Client } from '@auth0/auth0-spa-js';
import Configuration from '@/Configuration';

export interface OnRedirectCallback {
  (targetPath: string): void;
}

export interface AppState {
  targetPath: string;
}

export interface Auth0User {
  email: string;
  emailVerified: boolean;
  nickname: string;
  picture: string;
  sub: string;
  updatedAt: string;
}

class AuthService {
  private static instance: AuthService;

  private _isAuthenticated = false;

  private _user?: Auth0User;

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
    const appState: AppState = { targetPath };
    return this._auth0Client.loginWithRedirect({ appState });
  }

  private async handleReturn() {
    const appState: AppState = await this._auth0Client.handleRedirectCallback() as AppState;
    this._isAuthenticated = await this._auth0Client.isAuthenticated();
    this._user = AuthService.mapAuth0User(await this._auth0Client.getUser());
    AuthService.toggleVisibility();
    // console.log(this._user);
    // console.log(await this.getTokenSilently());
    const fallBackPath = '/';
    // Auth0 converts empty string (root path) to undefined.
    const targetPath = appState.targetPath ?? fallBackPath;
    // console.log(`Return to Coastline: ${targetPath}`);
    this._onRedirectCallback(targetPath);
  }

  private async refreshToken(): Promise<void> {
    await this.getTokenSilently();
    this._isAuthenticated = await this._auth0Client.isAuthenticated();
    this._user = AuthService.mapAuth0User(await this._auth0Client.getUser());
    AuthService.toggleVisibility();
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private static mapAuth0User(user: any): Auth0User {
    return {
      email: user.email,
      emailVerified: user.email_verified,
      nickname: user.nickname,
      picture: user.picture,
      sub: user.sub,
      updatedAt: user.updated_at,
    };
  }

  private static toggleVisibility() {
    const mainElement = document.getElementById('main');
    // eslint-disable-next-line @typescript-eslint/ban-ts-ignore
    // @ts-ignore
    mainElement.classList.toggle('not-visible');
  }
}

export default AuthService;
