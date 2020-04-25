import Vue from 'vue';
import VueMaterial from 'vue-material';
import 'vue-material/dist/vue-material.min.css';
import 'vue-material/dist/theme/default.css';
import Configuration from '@/Configuration';
import { Auth0Client } from '@auth0/auth0-spa-js';
import { createAuthService } from '@/auth/authServiceFactory';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';

const auth0Client = new Auth0Client({
  domain: Configuration.CONFIG.auth0.domain,
  // eslint-disable-next-line @typescript-eslint/camelcase
  client_id: Configuration.CONFIG.auth0.clientId,
  // audience: Configuration.CONFIG.auth0.audience,
  // eslint-disable-next-line @typescript-eslint/camelcase
  redirect_uri: Configuration.CONFIG.auth0.redirectUri,
});

const authService = createAuthService(auth0Client);

Vue.config.productionTip = false;

Vue.use(VueMaterial);

new Vue({
  router,
  render: (h) => h(App),
  created: () => {
    authService.setRedirectCallback((targetPath) => {
      router.push(targetPath);
    });
  },
}).$mount('#app');
