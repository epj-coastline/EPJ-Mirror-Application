import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Studygroups from '@/views/StudyGroups.vue';
import Coaching from '@/views/Coaching.vue';
import CoachingModule from '@/views/CoachingPerModule.vue';
import StudygroupsModule from '@/views/StudyGroupsPerModule.vue';
import Profile from '@/views/Profile.vue';
import authGuard from '@/auth/AuthGuard';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/', redirect: '/studygroups',
  },
  {
    path: '/studygroups',
    name: 'Lerngruppen',
    component: Studygroups,
  },
  {
    path: '/studygroups/:moduleId',
    name: 'LerngruppenProModul',
    component: StudygroupsModule,
  },
  {
    path: '/coaching',
    name: 'Nachhilfe',
    component: Coaching,
  },
  {
    path: '/coaching/:moduleId',
    name: 'NachhilfeProModul',
    component: CoachingModule,
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
