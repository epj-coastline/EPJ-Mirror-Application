import { expect } from 'chai';
import { User, validUser, validUsers } from '@/services/User';


describe('User type checking', () => {
  it('succeeds on correct data', () => {
    const testUser: User = {
      id: 's897dfnsf',
      firstName: 'Sebi',
      lastName: 'Hueber',
      email: 'seb@yolo.com',
      biography: 'hallo',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };

    const userValidity = validUser(testUser);
    expect(userValidity).to.be.equal(true);
  });
});

describe('User array type checking', () => {
  it('succeeds on correct data', () => {
    const testUsers: Array<User> = [
      {
        id: '23d4',
        firstName: 'Sebi',
        lastName: 'Hueber',
        email: 'seb@yolo.com',
        biography: 'hallo',
        degreeProgram: 'Informatik',
        startDate: 'HS18',
      },
      {
        id: '13e',
        firstName: 'Sebastian',
        lastName: 'Peter',
        email: 'seb@yovfvlo.com',
        biography: 'hallo 3',
        degreeProgram: 'Informatik',
        startDate: 'HS19',
      },
    ];

    const usersValidity = validUsers(testUsers);
    expect(usersValidity).to.be.equal(true);
  });
});
