<template>
  <md-list class="md-double-line">
    <md-subheader class="subheader">{{ title}}</md-subheader>
    <div v-for="module in modules" :key="module.id">
      <md-list-item class="cl-list-padding" :to="{ name: navigateTo, params: { moduleId: module.id, module: module }}" exact>
        <div class="md-list-item-text">
          <span>{{ module.token }} </span>
          <span>{{ module.name }}</span>
        </div>
        <md-icon>keyboard_arrow_right</md-icon>
      </md-list-item>
      <md-divider class="cl-inset"></md-divider>
    </div>
  </md-list>
</template>

<script lang="ts">
  import { Component, Prop, Vue } from 'vue-property-decorator';
  import { Module } from '@/services/Module';

  @Component({})
  export default class ModuleList extends Vue {
    @Prop()
    modules!: Array<Module>;

    @Prop()
    title!: string;

    @Prop({ required: true })
    navigateTo!: string;
  }
</script>

<style scoped lang="scss">
  .md-list-item-text > span:first-child{
    margin-bottom: 2px;
  }
  .subheader {
    min-height: 24px;
    color: rgba(0, 0, 0, 0.87);
    padding-left:   24px;
    padding-right:  24px;
  }
  .cl-inset {
    margin-left: 24px;
  }
  .cl-list-padding{
    padding-left:   8px;
    padding-right:  8px;
  }
  .md-subheader {
    padding-top: 24px;
    padding-bottom: 8px;
  }
</style>
