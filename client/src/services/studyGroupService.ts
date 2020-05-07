import Configuration from '@/Configuration';
import { plainToClass } from 'class-transformer';
import { StudyGroup, validStudyGroups } from '@/services/StudyGroup';
import { getAuthService } from '@/auth/authServiceFactory';
import { Auth0User } from '@/auth/interfaces';

class StudyGroupService {
  static getAll(): Promise<Array<StudyGroup>> {
    return fetch(`${Configuration.CONFIG.backendHost}/studygroups`, {
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
      .then((studyGroups: typeof StudyGroup[]) => plainToClass(StudyGroup, studyGroups,
        { excludeExtraneousValues: true }))
      .then((studyGroups) => {
        if (!validStudyGroups(studyGroups)) {
          throw new Error('Study groups are invalid.');
        }
        return studyGroups;
      });
  }

  static getPerModuleId(moduleId: number): Promise<Array<StudyGroup>> {
    return fetch(`${Configuration.CONFIG.backendHost}/studygroups?module=${moduleId}`, {
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
      .then((studyGroups: typeof StudyGroup[]) => plainToClass(StudyGroup, studyGroups,
        { excludeExtraneousValues: true }))
      .then((studyGroups) => {
        if (!validStudyGroups(studyGroups)) {
          throw new Error('Study groups are invalid.');
        }
        return studyGroups;
      });
  }

  static postStudyGroup(purpose: string, module: number) {
    const authService = getAuthService();
    const { user } = authService;
    // ToDo: use user.sub id
    // const userId = user.sub;
    const userId = -3;
    const moduleId = Number(module);
    const data = { purpose, userId, moduleId };

    // ToDo: add Error handling
    return fetch(`${Configuration.CONFIG.backendHost}/studygroups`, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
      },
      body: JSON.stringify(data),
    });
  }
}

export default StudyGroupService;
