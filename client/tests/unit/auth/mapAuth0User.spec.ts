import { expect } from 'chai';
import mapAuth0User from '@/auth/mapAuth0User';

describe('mapAuth0User', () => {
  it('maps successfully any to Auth0User', () => {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const anyUser: any = {
      email: 'marko@test.ch',
      // eslint-disable-next-line @typescript-eslint/camelcase
      email_verified: false,
      nickname: 'marko',
      picture: 'https//example.com/test.png',
      sub: 'auth0|5ea2cf4acd5dc90be8df47f3',
      // eslint-disable-next-line @typescript-eslint/camelcase
      updated_at: '2020-04-24T12:05:13.330Z',
    };
    const expectedId = '5ea2cf4acd5dc90be8df47f3';
    const mappedUser = mapAuth0User(anyUser);
    expect(mappedUser).to.include({
      email: anyUser.email,
      emailVerified: anyUser.email_verified,
      nickname: anyUser.nickname,
      picture: anyUser.picture,
      sub: anyUser.sub,
      updatedAt: anyUser.updated_at,
      id: expectedId,
    });
  });
  it('fails when a property has a wrong type', () => {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const anyUser: any = {
      email: 'marko@test.ch',
      // eslint-disable-next-line @typescript-eslint/camelcase
      email_verified: 'false',
      nickname: 'marko',
      picture: 'https//example.com/test.png',
      sub: 'auth0|5ea2cf4acd5dc90be8df47f3',
      // eslint-disable-next-line @typescript-eslint/camelcase
      updated_at: '2020-04-24T12:05:13.330Z',
    };
    expect(() => {
      mapAuth0User(anyUser);
    }).throw('Mapping to Auth0User failed');
  });
  it('fails when a property is missing', () => {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const anyUser: any = {
      email: 'marko@test.ch',
      // eslint-disable-next-line @typescript-eslint/camelcase
      nickname: 'marko',
      picture: 'https//example.com/test.png',
      sub: 'auth0|5ea2cf4acd5dc90be8df47f3',
      // eslint-disable-next-line @typescript-eslint/camelcase
      updated_at: '2020-04-24T12:05:13.330Z',
    };
    expect(() => {
      mapAuth0User(anyUser);
    }).throw('Mapping to Auth0User failed');
  });
});
