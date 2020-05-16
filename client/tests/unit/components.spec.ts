import { expect } from 'chai';
import { shallowMount } from '@vue/test-utils';
import ProfileImage from '@/components/common/ProfileImage.vue';
import StudyGroupList from '@/components/common/StudyGroupList.vue';
import ModuleList from '@/components/common/ModuleList.vue';
import UserList from '@/components/common/UserList.vue';
import StudyGroupCreateDialog from '@/components/common/StudyGroupCreateDialog.vue';
import { User } from '@/services/User';
import EditUserData from '@/components/common/EditUserData.vue';

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
    expect(result).to.be.equal('vor ein paar Sekunden');
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

    expect(wrapper.text()).to.be.equal('Alex Müller vor ein paar Sekunden Lorem ipsum dolor sit amet Alex Müller vor ein paar Sekunden sit amet');
  });
});

describe('EditUserData.vue', () => {
  it('renders structure correct', () => {
    const user: User = {
      id: 'usfitns',
      firstName: 'Alex',
      lastName: 'Müller',
      email: 'alex@hsr.ch',
      biography: 'hello',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };

    const wrapper = shallowMount(EditUserData, {
      propsData: { user },
    });
    expect(wrapper.text()).to.be.equal('VornameDer Vorname ist zu langNachnameDer Nachname ist zu langStudiengangBauingenieurwesenElektrotechnikErneuerbare Energien und UmwelttechnikInformatikLandschaftsarchitekturMaschinentechnik | InnovationStadt-, Verkehrs- und RaumplanungWirtschaftsingenieurwesenDer Studiengang wird benötigtStartFS16HS16FS17HS17FS18HS18FS19HS19FS20Das Startdatum wird benötigtSpeichern');
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

    expect(wrapper.text()).to.be.equal('Neue Lerngruppe für Xyclear Lerngruppe erstellen');
  });
});
