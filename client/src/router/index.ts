import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import StudyGroups from '@/views/StudyGroups.vue';
import Coaching from '@/views/Coaching.vue';
import Profile from '@/views/Profile.vue';
import authGuard from '@/auth/authGuard';
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
  {
    path: '/profile',
    name: 'profile',
    component: Profile,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach(authGuard);

export default router;
