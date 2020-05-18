import { plainToClass } from 'class-transformer';
import { Module, validModules } from '@/services/Module';
import FetchService from '@/services/fetchService';

class ModuleService {
  static async getAll(): Promise<Array<Module>> {
    const response = FetchService.get<Array<Module>>('modules');
    return this.mapToModulesAndValidate(response);
  }

  static async mapToModulesAndValidate(input: Promise<Array<Module>>): Promise<Array<Module>> {
    return input.then((modules) => plainToClass(Module, modules,
      { excludeExtraneousValues: true }))
      .then((modules) => {
        if (!validModules(modules)) {
          throw new Error('Modules are invalid.');
        }
        return modules;
      });
  }
}

export default ModuleService;
