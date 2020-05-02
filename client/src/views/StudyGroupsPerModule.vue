<template>
  <div>
    <Header :title="moduleTitel" sub-title="Wähle eine Lerngruppe." back-button="true"/>
    <StudyGroupList v-if="dataIsLoaded" :study-groups="studyGroups"/>
    <LoadingSpinner v-if="!dataIsLoaded"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue, Prop,
  } from 'vue-property-decorator';
  import Header from '@/components/layout/Header.vue';
  import { Module } from '@/services/Module';
  import StudyGroupList from '@/components/common/StudyGroupList.vue';
  import { StudyGroup, validStudyGroups } from '@/services/StudyGroup';
  import LoadingSpinner from '@/components/common/LoadingSpinner.vue';
  import StudyGroupService from '@/services/studyGroupService';

  @Component({
    components: {
      Header,
      StudyGroupList,
      LoadingSpinner,
    },
  })
  export default class StudyGroupsPerModule extends Vue {
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

    private studyGroups: Array<StudyGroup> = [];

    private dataIsLoaded = false;

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      if (this.internalModule.id === 9007199254740991) { // handle page reload
        await this.$router.push('/studygroups');
        return;
      }
      this.studyGroups = await StudyGroupService.getPerModuleId(this.internalModule.id);
      this.dataIsLoaded = validStudyGroups(this.studyGroups);
      if (!Array.isArray(this.studyGroups)) {
        await this.$router.push('/studygroups');
      }
    }

    get moduleTitel() {
      return `Modul ${this.internalModule.token}`;
    }
  }
</script>

<style scoped>

</style>
