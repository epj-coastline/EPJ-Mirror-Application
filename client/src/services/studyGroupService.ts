import { getAuthService } from '@/auth/authServiceFactory';
import Configuration from '@/Configuration';
import { plainToClass } from 'class-transformer';
import { StudyGroup, validStudyGroups } from '@/services/StudyGroup';

class StudyGroupService {
  static async getAll(): Promise<Array<StudyGroup>> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    return fetch(`${Configuration.CONFIG.backendHost}/studygroups`, {
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
      .then((studyGroups: typeof StudyGroup[]) => plainToClass(StudyGroup, studyGroups,
        { excludeExtraneousValues: true }))
      .then((studyGroups) => {
        if (!validStudyGroups(studyGroups)) {
          throw new Error('Study groups are invalid.');
        }
        return studyGroups;
      });
  }

  static async getPerModuleId(moduleId: number): Promise<Array<StudyGroup>> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    return fetch(`${Configuration.CONFIG.backendHost}/studygroups?module=${moduleId}`, {
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
      .then((studyGroups: typeof StudyGroup[]) => plainToClass(StudyGroup, studyGroups,
        { excludeExtraneousValues: true }))
      .then((studyGroups) => {
        if (!validStudyGroups(studyGroups)) {
          throw new Error('Study groups are invalid.');
        }
        return studyGroups;
      });
  }
}

export default StudyGroupService;
