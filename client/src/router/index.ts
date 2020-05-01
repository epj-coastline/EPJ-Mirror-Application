import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import StudyGroups from '@/views/StudyGroups.vue';
import Coaching from '@/views/Coaching.vue';
import CoachingPerModule from '@/views/CoachingPerModule.vue';
import StudyGroupsPerModule from '@/views/StudyGroupsPerModule.vue';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/', redirect: '/studygroups',
  },
  {
    path: '/studygroups',
    name: 'Lerngruppen',
    component: StudyGroups,
  },
  {
    path: '/studygroups/:moduleId',
    name: 'LerngruppenProModul',
    component: StudyGroupsPerModule,
    props: (route) => ({
      ...route.params,
    }),
  },
  {
    path: '/coaching',
    name: 'Nachhilfe',
    component: Coaching,
  },
  {
    path: '/coaching/:moduleId',
    name: 'NachhilfeProModul',
    component: CoachingPerModule,
    props: (route) => ({
      ...route.params,
    }),
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
