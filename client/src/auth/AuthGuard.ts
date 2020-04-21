import AuthService from '@/auth/AuthService';
import { NavigationGuard } from 'vue-router';

const authGuard: NavigationGuard = async (to, from, next) => {
  const authService = AuthService.getInstance();
  if (authService.isAuthenticated) {
    next();
  } else {
    try {
      await authService.handleAuthentication(to.fullPath);
    } catch (error) {
      // TODO: Error handling for authentication failure.
    }
    if (authService.isAuthenticated) {
      next();
    }
  }
};

export default authGuard;
