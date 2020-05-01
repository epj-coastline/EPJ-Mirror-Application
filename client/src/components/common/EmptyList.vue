<template>
  <div>
    <md-empty-state
      md-icon="list"
      :md-label=title
      :md-description=description>
    </md-empty-state>
  </div>
</template>

<script lang="ts">
  import { Component, Prop, Vue } from 'vue-property-decorator';


  @Component({ components: {} })
  export default class EmptyList extends Vue {
    @Prop()
    title!: string;

    @Prop()
    description!: string;
  }
</script>

<style scoped lang="scss">
</style>
