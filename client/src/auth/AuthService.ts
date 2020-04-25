import { Auth0Client } from '@auth0/auth0-spa-js';
import { Auth0AppState, Auth0OnRedirectCallback, Auth0User } from '@/auth/interfaces';
import { Vue } from 'vue-property-decorator';
import mapAuth0User from '@/auth/mapAuth0User';


class AuthService {
  private _auth0Client: Auth0Client;

  private _reactiveData: Vue;

  private _onRedirectCallback?: Auth0OnRedirectCallback;

  constructor(auth0Client: Auth0Client) {
    this._auth0Client = auth0Client;

    this._reactiveData = new Vue({
      data: {
        isAuthenticated: false,
        isLoading: true,
        user: undefined,
      },
    });
  }

  public get isLoading(): boolean {
    return this._reactiveData.$data.isLoading;
  }

  public get isAuthenticated(): boolean {
    return this._reactiveData.$data.isAuthenticated;
  }

  public get user(): Auth0User {
    return this._reactiveData.$data.user;
  }

  public setRedirectCallback(callback: Auth0OnRedirectCallback): void {
    this._onRedirectCallback = callback;
  }

  // Returns the access token. If the token is invalid or missing, a new one is retrieved.
  public getTokenAsync(): Promise<string> {
    return this._auth0Client.getTokenSilently();
  }

  public loginWithRedirect(targetPath: string): Promise<void> {
    const appState: Auth0AppState = { targetPath };
    return this._auth0Client.loginWithRedirect({ appState });
  }

  // Logs the user out and removes his session on the authorization server.
  public logout(): void {
    this._reactiveData.$data.isAuthenticated = false;
    this._auth0Client.logout();
  }

  public async handleAuthentication(targetPath: string): Promise<void> {
    this._reactiveData.$data.isLoading = true;
    try {
      // check if session  is still valid
      await this._auth0Client.getTokenSilently();
      await this.fetchAuthenticationInformation();
    } catch (error) {
      if (error.error === 'login_required') {
        await this.loginWithRedirect(targetPath);
      } else {
        throw error;
      }
    } finally {
      this._reactiveData.$data.isLoading = false;
    }
  }

  public async handleReturn(): Promise<void> {
    this._reactiveData.$data.isLoading = true;
    try {
      const result = await this._auth0Client.handleRedirectCallback();
      const appState: Auth0AppState = result.appState as Auth0AppState;
      await this.fetchAuthenticationInformation();
      if (this._onRedirectCallback) {
        this._onRedirectCallback(appState.targetPath);
      } else {
        throw new Error('onRedirectCallback is not set.');
      }
    } finally {
      this._reactiveData.$data.isLoading = false;
    }
  }

  private async fetchAuthenticationInformation(): Promise<void> {
    this._reactiveData.$data.isAuthenticated = await this._auth0Client.isAuthenticated();
    this._reactiveData.$data.user = mapAuth0User(await this._auth0Client.getUser());
  }
}

export default AuthService;
