<template>
  <div>
    <Header title="Lerngruppen" sub-title="Für welches Modul willst du Lernen?"/>
    <ModuleList v-if="dataIsLoaded" :modules="modules" title="Meine Module" navigate-to="LerngruppenProModul"/>
    <LoadingSpinner v-if="!dataIsLoaded"/>
  </div>
</template>

<script lang="ts">
  import { Component, Vue, Watch } from 'vue-property-decorator';
  import ModuleService from '@/services/moduleService';
  import { Module, validModules } from '@/services/Module';
  import ModuleList from '@/components/common/ModuleList.vue';
  import LoadingSpinner from '@/components/common/LoadingSpinner.vue';
  import Header from '@/components/layout/Header.vue';

  @Component({
    components: {
      Header,
      ModuleList,
      LoadingSpinner,
    },
  })
  export default class StudyGroups extends Vue {
    private modules: Array<Module> = [];

    private dataIsLoaded = false;

    @Watch('$route', { immediate: true, deep: true })
    async loadList() {
      this.modules = await ModuleService.getAll();
      this.dataIsLoaded = validModules(this.modules);
    }
  }
</script>

<style scoped>

</style>
