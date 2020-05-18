import { User, validUser, validUsers } from '@/services/User';
import { plainToClass } from 'class-transformer';
import { getAuthService } from '@/auth/authServiceFactory';
import FetchService from '@/services/fetchService';

class UserService {
  private static userInDatabase = false;

  static async getAll(): Promise<Array<User>> {
    const response = FetchService.get('users');
    return this.mapToUsersAndValidate(response);
  }

  static async getPerId(userId: string): Promise<User> {
    const response = FetchService.get(`users/${userId}`);
    return this.mapToUserAndValidate(response);
  }

  static async getPerStrength(moduleId: number): Promise<Array<User>> {
    const response = FetchService.get(`users?strength=${moduleId}`);
    return this.mapToUsersAndValidate(response);
  }

  static async getMyUser(): Promise<User> {
    return this.getPerId(getAuthService().user.id);
  }

  static async createUser() {
    return FetchService.post('users', this.newUserData());
  }

  static async updateUser(user: object): Promise<Response> {
    const userId = getAuthService().user.id;
    // Deep copy user object
    const body = JSON.parse(JSON.stringify(user));
    body.id = userId;
    return FetchService.put(`users/${userId}`, body);
  }

  static async checkAndAddUserToBackend() {
    if (!(await UserService.myUserExists())) {
      await UserService.createUser();
    }
  }

  static newUserData(): object {
    const authService = getAuthService();
    let nickName = authService.user.nickname;
    nickName = nickName.length <= 20 ? nickName : nickName.substring(0, 20);
    return {
      firstName: nickName,
      lastName: '',
      email: authService.user.email,
      biography: '',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  static async mapToUsersAndValidate(input: Promise<any>): Promise<Array<User>> {
    return input.then((users: typeof User[]) => plainToClass(User, users,
      { excludeExtraneousValues: true }))
      .then((users) => {
        if (!validUsers(users)) {
          throw new Error('Users are invalid.');
        }
        return users;
      });
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  static async mapToUserAndValidate(input: Promise<any>): Promise<User> {
    return input.then((user: typeof User) => plainToClass(User, user,
      { excludeExtraneousValues: true }))
      .then((user) => {
        if (!validUser(user)) {
          throw new Error('User does not exist.');
        }
        return user;
      });
  }

  static async myUserExists(): Promise<boolean> {
    // check cached fetch result from local variable
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
}

export default UserService;
