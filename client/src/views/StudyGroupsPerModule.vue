<template>
  <div>
    <Header :title="moduleTitel" sub-title="Wähle eine Lerngruppe oder erstelle eine neue." back-button="true"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue,
  } from 'vue-property-decorator';
  import Header from '@/components/layout/Header.vue';
  import Module from '@/services/Module';
  import ModuleService from '@/services/moduleService';

  @Component({
    components: {
      Header,
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
