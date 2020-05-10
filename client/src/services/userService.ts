import { User, validUsers } from '@/services/User';
import { plainToClass } from 'class-transformer';
import { getAuthService } from '@/auth/authServiceFactory';
import Configuration from '../Configuration';

class UserService {
  static async getAll(): Promise<Array<User>> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    return fetch(`${Configuration.CONFIG.backendHost}/users`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => {
        if (response.status === 200) {
          return Promise.resolve(response.json());
        }
        return Promise.resolve();
      })
      .then((users: typeof User[]) => plainToClass(User, users,
        { excludeExtraneousValues: true }))
      .then((users) => {
        if (!validUsers(users)) {
          throw new Error('Users are invalid.');
        }
        return users;
      });
  }

  static getPerStrength(moduleId: number): Promise<Array<User>> {
    return fetch(`${Configuration.CONFIG.backendHost}/users?strength=${moduleId}`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    })
      .then((response) => {
        if (response.status === 200) {
          return Promise.resolve(response.json());
        }
        return Promise.reject();
      })
      .then((users: typeof User[]) => plainToClass(User, users,
        { excludeExtraneousValues: true }))
      .then((users) => {
        if (!validUsers(users)) {
          throw new Error('Users are invalid.');
        }
        return users;
      });
  }
}

export default UserService;
