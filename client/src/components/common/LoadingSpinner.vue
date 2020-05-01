<template>
    <md-progress-spinner :md-diameter="100" :md-stroke="10" md-mode="indeterminate" class="cl-spinner"></md-progress-spinner>
</template>

<script lang="ts">
  import { Component, Vue } from 'vue-property-decorator';

  @Component({ components: {} })
  export default class LoadingSpinner extends Vue {}
</script>

<style scoped lang="scss">
  .cl-spinner {
    position: absolute;
    top: 50%;
    left: calc(50% - 50px);
  }
</style>
