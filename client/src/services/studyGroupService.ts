import Configuration from '@/Configuration';

class StudyGroupService {
  static getAll() {
    return fetch(`${Configuration.CONFIG.backendHost}/studygroups`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    }).then((response) => response.json())
      .then((result) => result);
  }
}
export default StudyGroupService;
