import Configuration from '../Configuration';

class UserService {
  static loadList() {
    return fetch(`${Configuration.CONFIG.backendHost}/users`, {
      method: 'GET',
      mode: 'cors',
      headers: {
        Accept: 'application/json',
      },
    })
      .then((response) => response.json())
      .then((result) => result);
  }
}

export default UserService;
