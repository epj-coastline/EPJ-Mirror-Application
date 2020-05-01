<template>
  <div>
    <Header title="Nachhilfe" sub-title="In welchem Modul benötigst du Hilfe?"/>
    <ModuleList v-if="dataIsLoaded" :modules="modules" title="Meine Module" navigate-to="NachhilfeProModul"/>
    <LoadingSpinner v-if="!dataIsLoaded"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue,
  } from 'vue-property-decorator';
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
  export default class Coaching extends Vue {
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
