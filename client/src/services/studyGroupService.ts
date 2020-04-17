import Configuration from '@/Configuration';
import { plainToClass } from 'class-transformer';
import { StudyGroup } from '@/services/StudyGroup';

class StudyGroupService {
  static getAll() {
    return fetch(`${Configuration.CONFIG.backendHost}/studygroups`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    }).then((response) => response.json())
      .then((users: typeof StudyGroup[]) => plainToClass(StudyGroup, users,
        { excludeExtraneousValues: true }));
  }
}
export default StudyGroupService;
