<template>
  <div>
    <Header title="Nachhilfe" sub-title="In welchem Modul benötigst du Hilfe?"/>
    <ModuleList :modules="modules" title="Meine Module" navigate-to="NachhilfeProModul"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue,
  } from 'vue-property-decorator';
  import ModuleService from '@/services/moduleService';
  import Module from '@/services/Module';
  import ModuleList from '@/components/common/ModuleList.vue';
  import Header from '@/components/layout/Header.vue';

  @Component({
    components: {
      Header,
      ModuleList,
    },
  })
  export default class Coaching extends Vue {
    private modules: Array<Module> = [];

    @Watch('$route', { immediate: true, deep: true })
    async loadList() {
      this.modules = await ModuleService.getAll();
    }
  }
</script>

<style scoped>

</style>
