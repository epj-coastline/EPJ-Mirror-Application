<template>
  <div>
    <Header :title="moduleTitel" :sub-title="numberOfStudents" back-button="true"/>
    <UserList :users="students" second-row-content="email"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue,
  } from 'vue-property-decorator';
  import UserService from '@/services/userService';
  import User from '@/services/User';
  import UserList from '@/components/common/UserList.vue';
  import Header from '@/components/layout/Header.vue';
  import Module from '@/services/Module';
  import ModuleService from '@/services/moduleService';

  @Component({
    components: {
      UserList,
      Header,
    },

  })
  export default class CoachingModule extends Vue {
    private students: Array<User> = [];

    // ToDo: Fix the empty state
    private module: Module = {
      id: 0,
      token: 'AA',
      name: 'Empty',
      responsibility: 'Empty',
    };

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      this.students = await UserService.getAll();
      this.module = await ModuleService.getModuleWithId();
    }

    get numberOfStudents() {
      return `${this.students.length} Studierende sind bereit, dir zu helfen.`;
    }

    get moduleTitel() {
      return `Modul ${this.module.token}`;
    }
  }
</script>

<style scoped>

</style>
