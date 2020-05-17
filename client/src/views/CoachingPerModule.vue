<template>
  <div>
    <Header :title="headerTitle" :sub-title="numberOfStudents" back-button="true"/>
    <UserList v-if="dataIsLoaded && !showEmptyList" :users="students" second-row-content="email"/>
    <EmptyList v-if="showEmptyList" title="Noch keine Studierende"
               description="Es gibt noch keine Studierende, welche dieses Modul zu ihren Stärken hinzugefügt haben."/>
    <LoadingSpinner v-if="!dataIsLoaded"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue, Prop,
  } from 'vue-property-decorator';
  import UserService from '@/services/userService';
  import { User } from '@/services/User';
  import UserList from '@/components/common/UserList.vue';
  import LoadingSpinner from '@/components/common/LoadingSpinner.vue';
  import Header from '@/components/layout/Header.vue';
  import { Module } from '@/services/Module';
  import EmptyList from '@/components/common/EmptyList.vue';
  import ModuleService from '@/services/moduleService';

  @Component({
    components: {
      EmptyList,
      LoadingSpinner,
      UserList,
      Header,
    },
  })

  export default class CoachingPerModule extends Vue {
    @Prop()
    module?: Module; // module is set if user selects a module in list

    @Prop()
    moduleId!: string; // moduleId from URL

    private moduleToken = this.module ? `${this.module.token}` : 'lädt...';

    private students: Array<User> = [];

    private dataIsLoaded = false;

    private showEmptyList = false;

    get headerTitle() {
      return this.module ? `Modul ${this.module.token}` : `Modul ${this.moduleToken}`;
    }

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      try {
        if (this.module === undefined) {
          await this.loadModuleData();
        }
        await this.loadUserData();
      } catch {
        await this.$router.push('/coaching');
      } finally {
        this.dataIsLoaded = true;
      }
    }

    private async loadUserData() {
      this.students = await UserService.getPerStrength(this.moduleIdAsNumber);
      if (this.students.length === 0) {
        this.showEmptyList = true;
      }
    }

    private async loadModuleData() {
      const result = await ModuleService.getAll();
      const foundModule = result.find((module) => module.id === this.moduleIdAsNumber);
      if (foundModule) {
        this.moduleToken = foundModule.token;
      }
    }

    get numberOfStudents() {
      const { length } = this.students;
      if (length === 1) {
        return `${length} Studierender ist bereit, dir zu helfen.`;
      }
      return `${length} Studierende sind bereit, dir zu helfen.`;
    }

    get moduleIdAsNumber() {
      return parseInt(this.moduleId, 10);
    }
  }
</script>

<style scoped>

</style>
