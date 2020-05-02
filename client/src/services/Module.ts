import { Expose } from 'class-transformer';

export class Module {
  @Expose() id!: number;

  @Expose() token!: string;

  @Expose() name!: string;

  @Expose() responsibility!: string;
}

 export function validModule(module: Module): boolean {
   if (typeof module === 'undefined') {
     return false;
   }
  return !(module.id === undefined
    || module.token === undefined
    || module.name === undefined
    || module.responsibility === undefined);
}

export function validModules(moduleList: Module[]) {
  if (typeof moduleList === 'undefined') {
    return false;
  }
  return moduleList.every(validModule);
}
