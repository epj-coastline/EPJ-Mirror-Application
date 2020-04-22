import { Auth0User } from '@/auth/Auth0User';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function mapAuth0User(user: any): Auth0User {
  return {
    email: user.email,
    emailVerified: user.email_verified,
    nickname: user.nickname,
    picture: user.picture,
    sub: user.sub,
    updatedAt: user.updated_at,
  };
}

export default mapAuth0User;
