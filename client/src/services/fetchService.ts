import { getAuthService } from '@/auth/authServiceFactory';
import Configuration from '@/Configuration';

class FetchService {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  static async get(path: string): Promise<any> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();

    return fetch(`${Configuration.CONFIG.backendHost}/${path}`, {
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
        return Promise.reject();
      });
  }

  static async post(path: string, data: object) {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();

    return fetch(`${Configuration.CONFIG.backendHost}/${path}`, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (!response.ok) {
          throw Error(response.statusText);
        }
        return Promise.resolve();
      });
  }

  static async put(path: string, data: object) {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();

    return fetch(`${Configuration.CONFIG.backendHost}/${path}`, {
      method: 'PUT',
      headers: {
        'content-type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (!response.ok) {
          throw Error(response.statusText);
        }
        return response;
      });
  }
}

export default FetchService;
