import Configuration from '@/Configuration';
import { plainToClass } from 'class-transformer';
import { StudyGroup, validStudyGroups } from '@/services/StudyGroup';
import { getAuthService } from '@/auth/authServiceFactory';
import { Auth0User } from '@/auth/interfaces';

class StudyGroupService {
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
      })
      .then((studyGroups) => {
        studyGroups.sort(
          (first: StudyGroup, second: StudyGroup) => this.compareDateDescending(first.creationDate, second.creationDate),
        );
        return studyGroups;
      });
  }

  public static compareDateDescending(date1: Date, date2: Date): number {
    const first = new Date(date1);
    const second = new Date(date2);
    return second.getTime() - first.getTime();
  }

  static postStudyGroup(purpose: string, module: number) {
    // ---- * ----
    // Todo: Remove this, and send token
    const authService = getAuthService();
    const { user } = authService;
    // const userId = user.sub;
    const userId = -3;
    // ---- * ----

    if (!purpose) {
      throw new Error('Purpose is invalid.');
    }
    const moduleId = Number(module);
    const data = { purpose, userId, moduleId };

    return fetch(`${Configuration.CONFIG.backendHost}/studygroups`, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
      },
      body: JSON.stringify(data),
    }).then((response) => {
      if (!response.ok) {
        throw Error(response.statusText);
      }
      return Promise.resolve();
    });
  }
}

export default StudyGroupService;
