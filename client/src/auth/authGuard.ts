import { NavigationGuard } from 'vue-router';
import { getAuthService } from '@/auth/authServiceFactory';
import UserService from '@/services/userService';

const authGuard: NavigationGuard = async (to, from, next) => {
  const authService = getAuthService();
  if (authService.isAuthenticated) {
    next();
  } else {
    try {
      if (window.location.search.includes('code=')) {
        // If the user is returning to the app after authentication
        await authService.handleReturn();
      } else {
        await authService.handleAuthentication(to.fullPath);
      }
    } catch (error) {
      throw new Error('Authentication has failed');
    }
    if (authService.isAuthenticated) {
      try {
        await UserService.checkAndAddUserToBackend();
      } catch {
        // ERROR
      }
      next();
    }
  }
};

export default authGuard;
