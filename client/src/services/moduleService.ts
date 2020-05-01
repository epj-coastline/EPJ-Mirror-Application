import Configuration from '@/Configuration';
import { plainToClass } from 'class-transformer';
import { Module } from '@/services/Module';

class ModuleService {
  static getAll() {
    return fetch(`${Configuration.CONFIG.backendHost}/modules`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    }).then((response) => response.json())
      .then((modules: typeof Module[]) => plainToClass(Module, modules,
        { excludeExtraneousValues: true }));
  }
}

export default ModuleService;
