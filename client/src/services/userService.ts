import { User } from '@/services/User';
import { plainToClass } from 'class-transformer';
import Configuration from '../Configuration';

class UserService {
  static async getAll(): Promise<Array<User>> {
    return fetch(`${Configuration.CONFIG.backendHost}/users`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    })
      .then((response) => {
        if (response.status === 200) {
          return Promise.resolve(response.json());
        }
        return Promise.resolve();
      })
      .then((users: typeof User[]) => plainToClass(User, users,
        { excludeExtraneousValues: true }));
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
        return Promise.resolve();
      })
      .then((users: typeof User[]) => plainToClass(User, users,
        { excludeExtraneousValues: true }));
  }
}

export default UserService;
