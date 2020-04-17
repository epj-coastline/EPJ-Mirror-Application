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
  import StudyGroupList from '@/components/common/StudyGroupList.vue';
  import StudyGroup from '@/services/StudyGroup';
  import StudyGroupService from '@/services/studyGroupService';

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

    private studyGroups: Array<StudyGroup> = [];

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      this.studyGroups = await StudyGroupService.getAll();
    }

    get moduleTitel() {
      return `Modul ${this.module.token}`;
    }
  }
</script>

<style scoped>

</style>
