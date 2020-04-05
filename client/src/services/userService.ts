import Configuration from '../Configuration';

class UserService {
  static getAll() {
    return fetch(`${Configuration.CONFIG.backendHost}/users`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
      },
    }).then((response) => response.json())
      .then((result) => result);
  }
}

export default UserService;
