import Configuration from '@/Configuration';
import { plainToClass } from 'class-transformer';
import { Module } from '@/services/Module';

class ModuleService {
  static getAll(): Promise<Array<Module>> {
    return fetch(`${Configuration.CONFIG.backendHost}/modules`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    })
      .then((response) => {
        if (response.status === 200) {
          return Promise.resolve(response.json());
        }
        return Promise.resolve();
      })
      .then((modules: typeof Module[]) => plainToClass(Module, modules,
        { excludeExtraneousValues: true }));
  }
}

export default ModuleService;
