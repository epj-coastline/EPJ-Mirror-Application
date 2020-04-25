import { expect } from 'chai';
import sinon from 'sinon';
import { Auth0Client } from '@auth0/auth0-spa-js';
import AuthService from '@/auth/AuthService';
import Auth0AppState from '@/auth/interfaces/Auth0AppState';

interface Auth0Error extends Error{
  error?: string;
}

describe('AuthService', () => {
  const testPath = 'test-path';

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const testUser: any = {
    email: 'marko@test.ch',
    // eslint-disable-next-line @typescript-eslint/camelcase
    email_verified: false,
    nickname: 'marko',
    picture: 'https//example.com/test.png',
    sub: 'auth0|5ea2cf4acd5dc90be8df47f3',
    // eslint-disable-next-line @typescript-eslint/camelcase
    updated_at: '2020-04-24T12:05:13.330Z',
  };

  const testIsAuthenticated = true;

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const auth0Mock: Auth0Client = {} as any as Auth0Client;

  auth0Mock.handleRedirectCallback = () => {
    const appState: Auth0AppState = {
      targetPath: testPath,
    };
    return Promise.resolve({ appState });
  };

  auth0Mock.isAuthenticated = () => Promise.resolve(testIsAuthenticated);

  auth0Mock.getUser = () => Promise.resolve(testUser);

  describe('constructor', () => {
    it('creates proper initial state', async () => {
      // Arrange
      const expectedIsAuthenticated = false;
      const expectedIsLoading = true;
      const expectedUser = undefined;
      // Act
      const authService = new AuthService(auth0Mock);
      // Assert
      expect(authService.isAuthenticated).to.be.equal(expectedIsAuthenticated);
      expect(authService.isLoading).to.be.equal(expectedIsLoading);
      expect(authService.user).to.be.equal(expectedUser);
    });
  });

  describe('loginWithRedirect', () => {
    it('should call _auth0Client.loginWithRedirect with the correct argument', async () => {
      // Arrange
      const loginWithRedirectSpy = sinon.spy();
      auth0Mock.loginWithRedirect = loginWithRedirectSpy;
      const authService = new AuthService(auth0Mock);
      const expectedArgument: Auth0AppState = { targetPath: testPath };
      // Act
      await authService.loginWithRedirect(testPath);
      // Assert
      expect(loginWithRedirectSpy.calledWith(expectedArgument));
    });
  });

  describe('logout', () => {
    it('should set isAuthenticated to false', async () => {
      // Arrange
      auth0Mock.logout = sinon.fake();
      const authService = new AuthService(auth0Mock);
      authService.setRedirectCallback(sinon.fake());
      await authService.handleReturn(); // Sets isAuthenticated to true
      // Act
      authService.logout();
      // Assert
      // eslint-disable-next-line no-unused-expressions
      expect(authService.isAuthenticated).to.be.false;
    });
    it('should call _auth0Client.logout()', () => {
      // Arrange
      const logoutSpy = sinon.spy();
      auth0Mock.logout = logoutSpy;
      const authService = new AuthService(auth0Mock);
      // Act
      authService.logout();
      // Assert
      // eslint-disable-next-line no-unused-expressions
      expect(logoutSpy.called).to.be.true;
    });
  });

  describe('handleAuthentication', () => {
    let authService: AuthService;

    beforeEach(() => {
      authService = new AuthService(auth0Mock);
    });

    it('should call _auth0Client.getTokenSilently', async () => {
      // Arrange
      const getTokenSilentlySpy = sinon.spy();
      auth0Mock.getTokenSilently = getTokenSilentlySpy;
      authService = new AuthService(auth0Mock);
      // Act
      await authService.handleAuthentication(testPath);
      // Assert
      // eslint-disable-next-line no-unused-expressions
      expect(getTokenSilentlySpy.called).to.be.true;
    });
    it('should fetch user information', async () => {
      // Act
      await authService.handleAuthentication(testPath);
      // Assert
      expect(authService.user.email).to.be.equal(testUser.email);
    });
    it('should fetch authentication information', async () => {
      // Act
      await authService.handleAuthentication(testPath);
      // Assert
      expect(authService.isAuthenticated).to.be.equal(testIsAuthenticated);
    });
    it('should call loginWithRedirect if error login_required occurs', () => {
      // Arrange
      const error: Auth0Error = new Error();
      error.error = 'login_required';
      auth0Mock.getTokenSilently = sinon.fake.throws(error);
      const loginWithRedirectSpy = sinon.spy();
      auth0Mock.loginWithRedirect = loginWithRedirectSpy;
      authService = new AuthService(auth0Mock);
      // Act
      authService.handleAuthentication(testPath);
      // Assert
      // eslint-disable-next-line no-unused-expressions
      expect(loginWithRedirectSpy.called).to.be.true;
    });
    it('should not call loginWithRedirect if no error login_required occurs', () => {
      // Arrange
      auth0Mock.getTokenSilently = sinon.fake();
      const loginWithRedirectSpy = sinon.spy();
      auth0Mock.loginWithRedirect = loginWithRedirectSpy;
      authService = new AuthService(auth0Mock);
      // Act
      authService.handleAuthentication(testPath);
      // Assert
      // eslint-disable-next-line no-unused-expressions
      expect(loginWithRedirectSpy.called).to.be.false;
    });
  });

  describe('handleReturn', () => {
    let authService: AuthService;

    beforeEach(() => {
      authService = new AuthService(auth0Mock);
    });

    it('throws an error if the onRedirectCallback is not set', async () => {
      try {
        // Act
        await authService.handleReturn();
      } catch (e) {
        // Assert
        expect(e.message).to.be.equal('onRedirectCallback is not set.');
      }
    });
    it('should call the onRedirectCallback with the correct argument', async () => {
      // Arrange
      const callbackSpy = sinon.spy();
      authService.setRedirectCallback(callbackSpy);
      // Act
      await authService.handleReturn();
      // Assert
      // eslint-disable-next-line no-unused-expressions
      expect(callbackSpy.calledWith(testPath)).to.be.true;
    });
    it('should fetch user information', async () => {
      // Arrange
      authService.setRedirectCallback(sinon.fake());
      // Act
      await authService.handleReturn();
      // Assert
      expect(authService.user.email).to.be.equal(testUser.email);
    });
    it('should fetch authentication information', async () => {
      // Arrange
      authService.setRedirectCallback(sinon.fake());
      // Act
      await authService.handleReturn();
      // Assert
      expect(authService.isAuthenticated).to.be.equal(testIsAuthenticated);
    });
  });
});
