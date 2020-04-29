import { Auth0User } from '@/auth/interfaces';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function convertToSTringSafe(possibleString: any): string {
  if (typeof possibleString === 'string') {
    return possibleString;
  }
  throw new Error('Conversion to string failed');
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function convertToBooleanSafe(possibleBoolean: any): boolean {
  if (typeof possibleBoolean === 'boolean') {
    return possibleBoolean;
  }
  throw new Error('Conversion to boolean failed');
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function mapAuth0User(user: any): Auth0User {
  try {
    return {
      email: convertToSTringSafe(user.email),
      emailVerified: convertToBooleanSafe(user.email_verified),
      nickname: convertToSTringSafe(user.nickname),
      picture: convertToSTringSafe(user.picture),
      sub: convertToSTringSafe(user.sub),
      updatedAt: convertToSTringSafe(user.updated_at),
    };
  } catch {
    throw new Error('Mapping to Auth0User failed');
  }
}

export default mapAuth0User;
