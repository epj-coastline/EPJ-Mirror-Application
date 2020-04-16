class ModuleService {
  static mockModuleListData: string = '[\n'
    + '{\n'
    + '  "id": "1",\n'
    + '  "token": "An1I",\n'
    + '  "name": "Analysis 1 für Informatiker",\n'
    + '  "responsibility": "Informatik"\n'
    + '},\n'
    + '{\n'
    + '"id": "2",\n'
    + '"token": "CN1",\n'
    + '"name": "Computernetze 1",\n'
    + '"responsibility": "Informatik"\n'
    + '},\n'
    + '{\n'
    + '"id": "3",\n'
    + '"token": "Dbs1",\n'
    + '"name": "Datenbanksysteme 1",\n'
    + '"responsibility": "Informatik"\n'
    + '}\n'
    + ']\n';

  static mockModule1: string = '{\n'
    + '  "id": "1",\n'
    + '  "token": "An1I",\n'
    + '  "name": "Analysis 1 für Informatiker",\n'
    + '  "responsibility": "Informatik"\n'
    + '}\n';

  static async getAll() {
    return JSON.parse(this.mockModuleListData);
  }

  static async getModuleWithId() {
    return JSON.parse(this.mockModule1);
  }
}

export default ModuleService;
