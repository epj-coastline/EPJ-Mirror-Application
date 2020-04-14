<template>
  <div>
    <Header title="Lerngruppen" sub-title="Für welches Modul willst du Lernen?"/>
    <h1>Meine Lerngruppen</h1>
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
  export default class Studygroups extends Vue {}
</script>

<style scoped>

</style>
