import { expect } from 'chai';
import { Auth0Client } from '@auth0/auth0-spa-js';
import { createAuthService, getAuthService } from '@/auth/authServiceFactory';
import AuthService from '@/auth/AuthService';

describe('authServiceFactory', () => {
  it('has the behaviour of a singleton', () => {
    expect(() => {
      getAuthService();
    }).throw('getAuthService was used before createAuthService was called');
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const auth0Mock: Auth0Client = {} as any as Auth0Client;
    const serviceA: AuthService = createAuthService(auth0Mock);
    const serviceB: AuthService = getAuthService();
    expect(serviceA).to.equal(serviceB);
    expect(() => {
      createAuthService(auth0Mock);
    }).throw('Only one instance of AuthService can exist');
  });
});
