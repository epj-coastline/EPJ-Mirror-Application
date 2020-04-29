import { Auth0Client } from '@auth0/auth0-spa-js';
import AuthService from '@/auth/AuthService';

let instance: AuthService;

export function createAuthService(auth0Client: Auth0Client) {
  if (!instance) {
    instance = new AuthService(auth0Client);
    return instance;
  }
  throw new Error('Only one instance of AuthService can exist');
}

export function getAuthService() {
  if (instance) {
    return instance;
  }
  throw new Error('getAuthService was used before createAuthService was called');
}
