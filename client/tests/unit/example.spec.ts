import { expect } from 'chai';
import { shallowMount } from '@vue/test-utils';
import HelloWorld from '@/components/HelloWorld.vue';
import ProfileImage from '@/components/common/ProfileImage.vue';
import StudyGroupList from '@/components/common/StudyGroupList.vue';
import ModuleList from '@/components/common/ModuleList.vue';


describe('HelloWorld.vue', () => {
  it('renders props.msg when passed', () => {
    const msg = 'new message';
    const wrapper = shallowMount(HelloWorld, {
      propsData: { msg },
    });
    expect(wrapper.text()).to.include(msg);
  });
});

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
