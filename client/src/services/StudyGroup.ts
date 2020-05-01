import { User, validUser } from '@/services/User';
import { Expose } from 'class-transformer';

export class StudyGroup {
  @Expose() id!: number;

  @Expose() purpose!: string;

  @Expose() creationDate!: Date;

  @Expose() user!: User;
}

export function validStudyGroup(studyGroup: StudyGroup): boolean {
  if (typeof studyGroup === 'undefined') {
    return false;
  }
  return !(studyGroup.id === undefined
    || studyGroup.purpose === undefined
    || studyGroup.creationDate === undefined
    || !validUser(studyGroup.user));
}

export function validStudyGroups(studyGroups: StudyGroup[]) {
  if (typeof studyGroups === 'undefined') {
    return false;
  }
  let returnValue = true;
  studyGroups.forEach((studyGroup) => {
    returnValue = returnValue && validStudyGroup(studyGroup);
  });
  return returnValue;
}
