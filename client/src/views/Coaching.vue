<template>
  <div>
    <Header title="Nachhilfe" sub-title="In welchem Modul benötigst du Hilfe?"/>
    <h1>Meine Nachhilfe</h1>
  </div>
</template>

<script lang="ts">
  import { Component, Vue } from 'vue-property-decorator';
  import Header from '@/components/layout/Header.vue';

  @Component({
    components: {
      Header,
    },
  })
  export default class Testing extends Vue {}
</script>

<style scoped>

</style>
