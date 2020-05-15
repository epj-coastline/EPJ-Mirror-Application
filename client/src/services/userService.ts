import { User, validUser, validUsers } from '@/services/User';
import { plainToClass } from 'class-transformer';
import { getAuthService } from '@/auth/authServiceFactory';
import Configuration from '../Configuration';

class UserService {
  private static userInDatabase = false;

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

  static async getPerId(userId: string): Promise<User> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    return fetch(`${Configuration.CONFIG.backendHost}/users/${userId}`, {
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
        return Promise.reject;
      })
      .then((user: typeof User) => plainToClass(User, user,
        { excludeExtraneousValues: true }))
      .then((user) => {
        if (!validUser(user)) {
          throw new Error('User does not exist.');
        }
        return user;
      });
  }

  static async getMyUser(): Promise<User> {
     return this.getPerId(getAuthService().user.id);
  }

  // cache fetch result in local variable
  static async myUserExists(): Promise<boolean> {
    if (this.userInDatabase) {
      return true;
    }
    try {
      return UserService.getMyUser()
      .then(
        () => {
          this.userInDatabase = true;
          return true;
        },
        () => false,
      );
    } catch (e) {
      return false;
    }
  }

  static async checkAndAddUserToBackend() {
    if (!(await UserService.myUserExists())) {
      await UserService.createUser();
    }
  }

  static async createUser() {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    const user = {
      firstName: authService.user.nickname,
      lastName: 'Nachname',
      email: authService.user.email,
      biography: '',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };

    return fetch(`${Configuration.CONFIG.backendHost}/users`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(user),
    })
      .then((response) => {
        if (!response.ok) {
          throw Error(response.statusText);
        }
        return response;
      });
  }

  static async updateUser(user: object): Promise<Response> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    const userId = authService.user.id;

    // Deep copy user object
    const body = JSON.parse(JSON.stringify(user));
    body.id = userId;

    return fetch(`${Configuration.CONFIG.backendHost}/users/${userId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(body),
    })
      .then((response) => {
        if (!response.ok) {
          throw Error(response.statusText);
        }
        return response;
      });
  }

  static async getPerStrength(moduleId: number): Promise<Array<User>> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    return fetch(`${Configuration.CONFIG.backendHost}/users?strength=${moduleId}`, {
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
