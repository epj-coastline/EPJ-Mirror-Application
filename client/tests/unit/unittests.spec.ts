import { expect } from 'chai';
import { shallowMount } from '@vue/test-utils';
import ProfileImage from '@/components/common/ProfileImage.vue';
import StudyGroupList from '@/components/common/StudyGroupList.vue';
import ModuleList from '@/components/common/ModuleList.vue';
import { User, validUser, validUsers } from '@/services/User';
import { StudyGroup, validStudyGroup, validStudyGroups } from '@/services/StudyGroup';
import UserList from '@/components/common/UserList.vue';
import { Module, validModule, validModules } from '@/services/Module';
import StudyGroupService from '@/services/studyGroupService';
import StudyGroupCreateDialog from '@/components/common/StudyGroupCreateDialog.vue';

describe('ProfileImage.vue', () => {
  it('renders Initials when passed', () => {
    const firstName = 'Hanspeter';
    const lastName = 'Burri';
    const wrapper = shallowMount(ProfileImage, {
      propsData: { firstName, lastName },
    });
    expect(wrapper.text()).to.be.equal('HB');
  });
});

describe('StudyGroupList.vue', () => {
  it('renders formatted date when passed', () => {
    const creator1 = {
      id: 2,
      firstName: 'Alex',
      lastName: 'Müller',
      email: 'alex@hsr.ch',
      biography: 'hello',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };
    const studyGroup = {
      id: 1,
      purpose: 'Lorem ipsum dolor sit amet',
      creationDate: new Date(),
      user: creator1,
    };

    const wrapper = shallowMount(StudyGroupList, {
      propsData: { studyGroups: [studyGroup] },
    });
    const node = wrapper.element.getElementsByClassName('md-subhead').item(0);
    let result = '';
    if (node != null && node.textContent != null) {
      result = node.textContent;
    }
    expect(result).to.be.equal('a few seconds ago');
  });

  it('renders study group information when passed', () => {
    const creator1 = {
      id: 2,
      firstName: 'Alex',
      lastName: 'Müller',
      email: 'alex@hsr.ch',
      biography: 'hello',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };
    const studyGroup1 = {
      id: 1,
      purpose: 'Lorem ipsum dolor sit amet',
      creationDate: new Date(),
      user: creator1,
    };
    const studyGroup2 = {
      id: 2,
      purpose: 'sit amet',
      creationDate: new Date(),
      user: creator1,
    };

    const wrapper = shallowMount(StudyGroupList, {
      propsData: { studyGroups: [studyGroup1, studyGroup2] },
    });

    expect(wrapper.text()).to.be.equal('Alex Müller a few seconds ago Lorem ipsum dolor sit amet Alex Müller a few seconds ago sit amet');
  });
});

describe('ModuleList.vue', () => {
  it('renders module information when passed', () => {
    const module1 = {
      id: 1,
      token: 'An1I',
      name: 'Analysis 1 für Informatiker',
      responsibility: 'Informatik',
    };
    const module2 = {
      id: 1,
      token: 'An1I',
      name: 'Analysis 1 für Informatiker',
      responsibility: 'Informatik',
    };

    const wrapper = shallowMount(ModuleList, {
      propsData: {
        modules: [module1, module2],
        title: 'Xy',
        navigateTo: 'Ab',
      },
    });

    expect(wrapper.text()).to.be.equal('XyAn1I Analysis 1 für Informatikerkeyboard_arrow_rightAn1I Analysis 1 für Informatikerkeyboard_arrow_right');
  });
});

describe('UserList.vue', () => {
  it('renders users with information when passed', () => {
    const user1 = {
      id: 2,
      firstName: 'Alex',
      lastName: 'Müller',
      email: 'alex@hsr.ch',
      biography: 'hello',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };
    const user2 = {
      id: 3,
      firstName: 'Hans',
      lastName: 'Peters',
      email: 'hans@hsr.ch',
      biography: 'hello',
      degreeProgram: 'Informatik',
      startDate: 'HS19',
    };

    const wrapper = shallowMount(UserList, {
      propsData: {
        users: [user1, user2],
        title: 'Xy',
      },
    });

    expect(wrapper.text()).to.be.equal('XyAlex Müller InformatikHans Peters Informatik');
  });
});

describe('StudyGroupCreateDialog.vue', () => {
  it('renders dialog window when passed', () => {
    const wrapper = shallowMount(StudyGroupCreateDialog, {
      propsData: {
        moduleId: 1,
        moduleTitle: 'Xy',
      },
    });

    expect(wrapper.text()).to.be.equal('Neue Lerngruppe für XyclearLerngruppe erstellen');
  });
});

describe('User type checking', () => {
  it('succeeds on correct data', () => {
    const testUser: User = {
      id: 12,
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
        id: 12,
        firstName: 'Sebi',
        lastName: 'Hueber',
        email: 'seb@yolo.com',
        biography: 'hallo',
        degreeProgram: 'Informatik',
        startDate: 'HS18',
      },
      {
        id: 14,
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

describe('Module type checking', () => {
  it('succeeds on correct data', () => {
    const testModule: Module = {
      id: 12,
      token: 'Infsi3',
      name: 'Informationssicherheit 3',
      responsibility: 'Informatik',
    };

    const moduleValidity = validModule(testModule);
    expect(moduleValidity).to.be.equal(true);
  });
});


describe('Module Array type checking', () => {
  it('succeeds on correct data', () => {
    const testModules: Array<Module> = [
      {
        id: 12,
        token: 'Infsi3',
        name: 'Informationssicherheit 3',
        responsibility: 'Informatik',
      },
      {
        id: 1,
        token: 'Cpp',
        name: 'C++ 3',
        responsibility: 'Informatik',
      },
    ];

    const moduleValidity = validModules(testModules);
    expect(moduleValidity).to.be.equal(true);
  });
});

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

describe('sort two dates in descending order', () => {
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
