<template>
  <div>
    <Header title="Lerngruppen" sub-title="Für welches Modul willst du Lernen?"/>
    <ModuleList :modules="modules" title="Meine Module" navigate-to="LerngruppenProModul"/>
  </div>
</template>

<script lang="ts">
  import { Component, Vue, Watch } from 'vue-property-decorator';
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
  export default class StudyGroups extends Vue {
    private modules: Array<Module> = [];

    @Watch('$route', { immediate: true, deep: true })
    async loadList() {
      this.modules = await ModuleService.getAll();
    }
  }
</script>

<style scoped>

</style>
