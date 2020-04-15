import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import StudentOne from '@/views/Student.vue';
import Studygroups from '@/views/Studygroups.vue';
import Coaching from '@/views/Coaching.vue';
import CoachingModule from '@/views/CoachingModule.vue';
import Testing from '@/views/Testing.vue';
import StudygroupsModule from '@/views/StudygroupsModule.vue';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/', redirect: '/studygroups',
  },
  {
    path: '/students',
    name: 'Studenten',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Students.vue'),
  },
  {
    path: '/students/:userId',
    name: 'Student',
    component: StudentOne,
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
    path: '/experimental',
    name: 'Experimental',
    component: Testing,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
