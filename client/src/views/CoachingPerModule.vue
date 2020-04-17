<template>
  <div>
    <Header :title="moduleTitel" :sub-title="numberOfStudents" back-button="true"/>
    <UserList v-if="dataIsLoaded" :users="students" second-row-content="email"/>
    <LoadingSpinner v-if="!dataIsLoaded"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue,
  } from 'vue-property-decorator';
  import UserService from '@/services/userService';
  import { User, validUsers } from '@/services/User';
  import UserList from '@/components/common/UserList.vue';
  import LoadingSpinner from '@/components/common/LoadingSpinner.vue';
  import Header from '@/components/layout/Header.vue';
  import Module from '@/services/Module';
  import ModuleService from '@/services/moduleService';

  @Component({
    components: {
      LoadingSpinner,
      UserList,
      Header,
    },

  })
  export default class CoachingPerModule extends Vue {
    private students: Array<User> = [];

    private dataIsLoaded = false;

    // ToDo: Fix the empty state
    private module: Module = {
      id: 0,
      token: 'lädt...',
      name: 'Empty',
      responsibility: 'Empty',
    };

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      this.students = await UserService.getAll();
      this.dataIsLoaded = validUsers(this.students);
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
