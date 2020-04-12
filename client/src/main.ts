import Vue from 'vue';
import VueMaterial from 'vue-material';
import 'vue-material/dist/vue-material.min.css';
import 'vue-material/dist/theme/default.css';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';

require('dotenv').config();

Vue.config.productionTip = false;

Vue.use(VueMaterial);

new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app');
