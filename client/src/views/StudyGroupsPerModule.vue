<template>
  <div>
    <Header :title="moduleTitel" sub-title="Wähle eine Lerngruppe oder erstelle eine neue." back-button="true"/>
    <StudyGroupList :study-groups="studyGroups"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue,
  } from 'vue-property-decorator';
  import Header from '@/components/layout/Header.vue';
  import Module from '@/services/Module';
  import ModuleService from '@/services/moduleService';
  import StudyGroupList from '@/components/common/StudyGroupList.vue';
  import StudyGroup from '@/services/StudyGroup';
  import User from '@/services/User';

  @Component({
    components: {
      Header,
      StudyGroupList,
    },

  })
  export default class StudyGroupsPerModule extends Vue {
    // ToDo: Fix the empty state
    private module: Module = {
      id: 0,
      token: 'AA',
      name: 'Empty',
      responsibility: 'Empty',
    };

    // ToDo: Create Service and replace Fake Data
    /* ---- Fake Data ---- */
    private creator: User = {
      id: 1,
      firstName: 'Eliane',
      lastName: 'Schmidli',
      email: 'eliane.schmidli@hsr.ch',
      biography: 'hello',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };

    private creator2: User = {
      id: 2,
      firstName: 'Alex',
      lastName: 'Müller',
      email: 'alex@hsr.ch',
      biography: 'hello',
      degreeProgram: 'Informatik',
      startDate: 'HS18',
    };

    private studyGroup: StudyGroup = {
      id: 1,
      purpose: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Optio itaque ea nostrum. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Optio itaque ea nostrum.',
      creationDate: new Date(),
      creator: this.creator,
      module: this.module,
    };

    private studyGroup2: StudyGroup = {
      id: 2,
      purpose: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Optio itaque ea nostrum. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Optio itaque ea nostrum.',
      creationDate: new Date('2020-04-15'),
      creator: this.creator2,
      module: this.module,
    };

    private studyGroups: Array<StudyGroup> = [this.studyGroup, this.studyGroup2];
    /* ---- end ---- */

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      this.module = await ModuleService.getModuleWithId();
    }

    get moduleTitel() {
      return `Modul ${this.module.token}`;
    }
  }
</script>

<style scoped>

</style>
