import Configuration from '@/Configuration';
import { plainToClass } from 'class-transformer';
import { Module, validModules } from '@/services/Module';
import { getAuthService } from '@/auth/authServiceFactory';

class ModuleService {
  static async getAll(): Promise<Array<Module>> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    return fetch(`${Configuration.CONFIG.backendHost}/modules`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => {
        if (response.status === 200) {
          return Promise.resolve(response.json());
        }
        return Promise.resolve();
      })
      .then((modules: typeof Module[]) => plainToClass(Module, modules,
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
