import { getAuthService } from '@/auth/authServiceFactory';
import Configuration from '@/Configuration';
import { plainToClass } from 'class-transformer';
import { StudyGroup, validStudyGroups } from '@/services/study-group/StudyGroup';
import StudyGroupForCreation from '@/services/study-group/StudyGroupForCreation';

class StudyGroupService {
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
        if (response.ok) {
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

  static async postStudyGroup(purpose: string, moduleId: string) {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    const numberModuleId = Number(moduleId);
    const data: StudyGroupForCreation = { purpose, moduleId: numberModuleId };

    return fetch(`${Configuration.CONFIG.backendHost}/studygroups`, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        Authorization: `Bearer ${token}`,
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
