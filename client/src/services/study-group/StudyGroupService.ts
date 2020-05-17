import { plainToClass } from 'class-transformer';
import { StudyGroup, validStudyGroups } from '@/services/study-group/StudyGroup';
import StudyGroupForCreation from '@/services/study-group/StudyGroupForCreation';
import FetchService from '@/services/fetchService';

class StudyGroupService {
  static async getPerModuleId(moduleId: number): Promise<Array<StudyGroup>> {
    const response = FetchService.get(`studygroups?module=${moduleId}`);
    const studyGroups = this.mapToStudyGroupsAndValidate(response);
    return this.sortStudyGroups(studyGroups);
  }

  static async postStudyGroup(purpose: string, moduleId: string) {
    const numberModuleId = Number(moduleId);
    const data: StudyGroupForCreation = { purpose, moduleId: numberModuleId };
    return FetchService.post('studygroups', data);
  }

  static async sortStudyGroups(input: Promise<Array<StudyGroup>>): Promise<Array<StudyGroup>> {
    return input.then((studyGroups) => {
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

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  static async mapToStudyGroupsAndValidate(input: Promise<any>): Promise<Array<StudyGroup>> {
    return input.then((studyGroups: typeof StudyGroup[]) => plainToClass(StudyGroup, studyGroups,
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
