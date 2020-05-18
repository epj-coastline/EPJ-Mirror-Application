import { getAuthService } from '@/auth/authServiceFactory';
import Configuration from '@/Configuration';

class FetchService {
  static async get<T>(path: string): Promise<T> {
    return fetch(`${Configuration.CONFIG.backendHost}/${path}`, {
      method: 'GET',
      headers: await this.getHeaders(),
    }).then((response) => {
        if (response.ok) {
          return Promise.resolve(response.json());
        }
        return Promise.reject();
      });
  }

  static async post(path: string, data: object) {
    return fetch(`${Configuration.CONFIG.backendHost}/${path}`, {
      method: 'POST',
      headers: await this.getHeaders(),
      body: JSON.stringify(data),
    }).then((response) => {
        if (!response.ok) {
          throw Error(response.statusText);
        }
        return Promise.resolve();
      });
  }

  static async put(path: string, data: object) {
    return fetch(`${Configuration.CONFIG.backendHost}/${path}`, {
      method: 'PUT',
      headers: await this.getHeaders(),
      body: JSON.stringify(data),
    }).then((response) => {
        if (!response.ok) {
          throw Error(response.statusText);
        }
        return response;
      });
  }

  static async getHeaders(): Promise<Headers> {
    const authService = getAuthService();
    const token = await authService.getTokenAsync();
    const requestHeaders = new Headers();
    requestHeaders.append('content-type', 'application/json');
    requestHeaders.append('Authorization', `Bearer ${token}`);
    return requestHeaders;
  }
}

export default FetchService;
