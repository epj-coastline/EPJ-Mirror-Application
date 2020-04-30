import { User } from '@/services/User';
import { plainToClass } from 'class-transformer';
import Configuration from '../Configuration';

class UserService {
  static async getAll() {
    return fetch(`${Configuration.CONFIG.backendHost}/users`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    })
      .then((response) => response.json())
      .then((users: typeof User[]) => plainToClass(User, users,
        { excludeExtraneousValues: true }));
  }

  static getPerStrength(moduleId: number) {
    return fetch(`${Configuration.CONFIG.backendHost}/users?strength=${moduleId}`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    }).then((response) => response.json())
      .then((users: typeof User[]) => plainToClass(User, users,
        { excludeExtraneousValues: true }));
  }
}

export default UserService;
