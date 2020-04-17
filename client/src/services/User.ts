import { Expose } from 'class-transformer';

export class User {
  @Expose() id!: number;

  @Expose() firstName!: string;

  @Expose() lastName!: string;

  @Expose() email!: string;

  @Expose() biography!: string;

  @Expose() degreeProgram!: string;

  @Expose() startDate!: string;
}

export function validUser(user: User): boolean {
  return !(user.id === undefined
    || user.firstName === undefined
    || user.lastName === undefined
    || user.email === undefined
    || user.biography === undefined
    || user.degreeProgram === undefined
    || user.startDate === undefined);
}

export function validUsers(users: User[]) {
  let returnValue = true;
  users.forEach((user) => {
    returnValue = returnValue && validUser(user);
  });
  return returnValue;
}
