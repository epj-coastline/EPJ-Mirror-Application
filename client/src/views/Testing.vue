<template>
  <div>
    <Header title="Empty View"/>
    <h1>This can be used for testing</h1>
  </div>
</template>

<style scoped>

</style>

<script lang="ts">
  import {
    Component, Vue,
  } from 'vue-property-decorator';
  import Header from '@/components/layout/Header.vue';

  @Component({
    components: {
      Header,
    },
  })
  export default class Testing extends Vue {}
</script>
