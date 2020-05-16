import { expect } from 'chai';
import { Module, validModule, validModules } from '@/services/Module';

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
