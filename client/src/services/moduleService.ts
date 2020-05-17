import { plainToClass } from 'class-transformer';
import { Module, validModules } from '@/services/Module';
import FetchService from '@/services/fetchService';

class ModuleService {
  static async getAll(): Promise<Array<Module>> {
    const response = FetchService.get('modules');
    return this.mapToModulesAndValidate(response);
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  static async mapToModulesAndValidate(input: Promise<any>): Promise<Array<Module>> {
    return input.then((modules: typeof Module[]) => plainToClass(Module, modules,
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
