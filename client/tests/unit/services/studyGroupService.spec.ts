import { expect } from 'chai';
import { StudyGroup, validStudyGroup, validStudyGroups } from '@/services/StudyGroup';
import StudyGroupService from '@/services/studyGroupService';


describe('Study Group type checking', () => {
  it('succeeds on correct data', () => {
    const testStudyGroup: StudyGroup = {
      id: 12,
      purpose: 'none',
      creationDate: new Date(41241234124),
      user: {
        id: 12,
        firstName: 'Sebi',
        lastName: 'Hueber',
        email: 'seb@yolo.com',
        biography: 'hallo',
        degreeProgram: 'Informatik',
        startDate: 'HS18',
      },
    };

    const studyGroupValidity = validStudyGroup(testStudyGroup);
    expect(studyGroupValidity).to.be.equal(true);
  });
});

describe('Study Group Array type checking', () => {
  it('succeeds on correct data', () => {
    const testStudyGroups: Array<StudyGroup> = [
      {
        id: 12,
        purpose: 'none',
        creationDate: new Date(41241234124),
        user: {
          id: 12,
          firstName: 'Sebi',
          lastName: 'Hueber',
          email: 'seb@yolo.com',
          biography: 'hallo',
          degreeProgram: 'Informatik',
          startDate: 'HS18',
        },
      },
      {
        id: 22,
        purpose: 'ne',
        creationDate: new Date(41241224),
        user: {
          id: 14,
          firstName: 'Seba',
          lastName: 'Hur',
          email: 'seb@ycdcdolo.com',
          biography: 'hao',
          degreeProgram: 'Informatik',
          startDate: 'HS20',
        },
      },
    ];

    const studyGroupsValidity = validStudyGroups(testStudyGroups);
    expect(studyGroupsValidity).to.be.equal(true);
  });
});

describe('Sort two dates in descending order', () => {
  it('succeeds on same dates', () => {
    const date1 = new Date('2020-05-07T14:17:00.97696');
    expect(StudyGroupService.compareDateDescending(date1, date1)).to.be.equal(0);
  });
  it('succeeds on different dates', () => {
    const newer = new Date('2020-05-07T14:17:00.97696');
    const older = new Date('2020-05-06T14:17:00.97696');
    expect(StudyGroupService.compareDateDescending(newer, older)).to.be.lessThan(0);
  });
  it('succeeds on same dates and different times', () => {
    const newer = new Date('2020-05-07T14:17:00.97696');
    const older = new Date('2020-05-07T13:17:00.97696');
    expect(StudyGroupService.compareDateDescending(newer, older)).to.be.lessThan(0);
  });
});
