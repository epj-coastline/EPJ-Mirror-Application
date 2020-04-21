import Vue from 'vue';
import VueMaterial from 'vue-material';
import 'vue-material/dist/vue-material.min.css';
import 'vue-material/dist/theme/default.css';
import AuthService from '@/auth/AuthService';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';

Vue.config.productionTip = false;

Vue.use(VueMaterial);

new Vue({
  router,
  render: (h) => h(App),
  created: () => {
    const authService = AuthService.getInstance();
    authService.setRedirectCallback((targetPath) => {
      router.push(targetPath);
    });
  },
}).$mount('#app');
