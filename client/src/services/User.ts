import { Expose } from 'class-transformer';

export class User {
  @Expose() id!: string;

  @Expose() firstName!: string;

  @Expose() lastName!: string;

  @Expose() email!: string;

  @Expose() biography!: string;

  @Expose() degreeProgram!: string;

  @Expose() startDate!: string;
}

export function validUser(user: User): boolean {
  if (typeof user === 'undefined') {
    return false;
  }
  return !(user.id === undefined
    || user.firstName === undefined
    || user.lastName === undefined
    || user.email === undefined
    || user.biography === undefined
    || user.degreeProgram === undefined
    || user.startDate === undefined);
}

export function validUsers(users: User[]) {
  if (typeof users === 'undefined') {
    return false;
  }
  return users.every(validUser);
}
