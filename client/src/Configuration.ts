import dotenv from 'dotenv';

dotenv.config();

export default class Configuration {
  static get CONFIG() {
    return {
      backendHost: '$API_URL',
    };
  }
}
