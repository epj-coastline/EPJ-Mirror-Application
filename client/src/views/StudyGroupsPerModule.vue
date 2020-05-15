<template>
  <div>
    <Header :title="moduleTitle" sub-title="Wähle eine Lerngruppe." back-button="true"/>
    <StudyGroupList v-if="dataIsLoaded" :study-groups="studyGroups"/>
    <EmptyList v-if="showEmptyList"
               title="Noch keine Lerngruppen"
               description="Es gibt noch keine Lerngruppen für dieses Modul. Erstelle hier die Erste."/>
    <LoadingSpinner v-if="!dataIsLoaded"/>
    <div>
      <md-button @click="openStudyGroupCreateDialog" class="md-fab md-primary md-fab-bottom-right md-fixed cl-floating-action">
        <md-icon>add</md-icon>
      </md-button>
    </div>
    <StudyGroupCreateDialog :moduleId="moduleId"
                            :module-title="moduleTitle"
                            v-on:closeCreateDialog="closeStudyGroupCrateDialog"
                            v-if="studyGroupCreation"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue, Prop,
  } from 'vue-property-decorator';
  import Header from '@/components/layout/Header.vue';
  import { Module } from '@/services/Module';
  import StudyGroupList from '@/components/common/StudyGroupList.vue';
  import { StudyGroup } from '@/services/StudyGroup';
  import LoadingSpinner from '@/components/common/LoadingSpinner.vue';
  import StudyGroupService from '@/services/studyGroupService';
  import ModuleService from '@/services/moduleService';
  import StudyGroupCreateDialog from '@/components/common/StudyGroupCreateDialog.vue';
  import EmptyList from '@/components/common/EmptyList.vue';

  @Component({
    components: {
      StudyGroupCreateDialog,
      Header,
      StudyGroupList,
      LoadingSpinner,
      EmptyList,
    },
  })

  export default class StudyGroupsPerModule extends Vue {
    @Prop()
    module!: Module;

    @Prop()
    moduleId!: string; // moduleID from URL

    private moduleTitle = this.module ? `Modul ${this.module.token}` : 'Modul lädt...';

    private studyGroups: Array<StudyGroup> = [];

    private dataIsLoaded = false;

    private studyGroupCreation = false;

    private showEmptyList = false;

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      try {
        if (this.module === undefined) {
          await this.loadModuleData();
        }
        this.studyGroups = await StudyGroupService.getPerModuleId(this.moduleIdAsNumber);

        if (this.studyGroups.length === 0) {
          this.showEmptyList = true;
        } else {
          this.showEmptyList = false;
        }
      } catch {
        await this.$router.push('/studygroups');
      } finally {
        this.dataIsLoaded = true;
      }
    }

    private async loadModuleData() {
      const result = await ModuleService.getAll();
      const foundModule = result.find((module) => module.id === this.moduleIdAsNumber);
      if (foundModule) {
        this.moduleTitle = foundModule.token;
      }
    }

    get moduleIdAsNumber() {
      return parseInt(this.moduleId, 10);
    }

    private openStudyGroupCreateDialog() {
      this.studyGroupCreation = true;
    }

    public closeStudyGroupCrateDialog() {
      this.studyGroupCreation = false;
      this.loadData();
    }
  }
</script>

<style scoped>
  .cl-floating-action {
     bottom: 80px;
  }
</style>
