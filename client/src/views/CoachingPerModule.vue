<template>
  <div>
    <Header :title="moduleTitel" :sub-title="numberOfStudents" back-button="true"/>
    <UserList v-if="dataIsLoaded" :users="students" second-row-content="email"/>
    <LoadingSpinner v-if="!dataIsLoaded"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue, Prop,
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
     @Prop({
      default() {
        return {
          id: 9007199254740991, // ToDo: this checking integer should be defined outside, so it doesn't look like magic
          token: 'lädt...', // used as loading text, in case it needs to fetch the module when page reloaded
          name: 'Empty',
          responsibility: 'Empty',
        };
      },
    })
    module!: Module;

    @Prop()
    moduleId!: number; // moduleID from URL in case of a page reload

    private internalModule: Module = this.module; // so we don't modify the prop data below

    private students: Array<User> = [];

    private dataIsLoaded = false;

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      if (this.internalModule.id === 9007199254740991) { // handle page reload
         await this.$router.push('/coaching');
      }
      this.students = await UserService.getPerStrength(this.internalModule.id);
      this.dataIsLoaded = validUsers(this.students);
    }

    get numberOfStudents() {
      const { length } = this.students;
      if (length === 1) {
        return `${length} Studierender ist bereit, dir zu helfen.`;
      }
      return `${length} Studierende sind bereit, dir zu helfen.`;
    }

    get moduleTitel() {
      return `Modul ${this.internalModule.token}`;
    }
  }
</script>

<style scoped>

</style>
