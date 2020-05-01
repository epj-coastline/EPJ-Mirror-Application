import { Expose } from 'class-transformer';

export class Module {
  @Expose() id!: number;

  @Expose() token!: string;

  @Expose() name!: string;

  @Expose() responsibility!: string;
}

 export function validModule(module: Module): boolean {
  return !(module.id === undefined
    || module.token === undefined
    || module.name === undefined
    || module.responsibility === undefined);
}

export function validModules(moduleList: Module[]) {
  let returnValue = true;
  moduleList.forEach((module) => {
    returnValue = returnValue && validModule(module);
  });
  return returnValue;
}
