<template>
  <div>
    <md-toolbar class="md-primary cl-header">
      <div class="cl-first-row">
        <md-button v-if="backButton" @click=$router.go(-1) class="md-icon-button cl-back-button">
          <md-icon>chevron_left</md-icon>
        </md-button>
        <h3 class="md-title cl-first-row-title">{{title}}</h3>
        <md-button v-if="actionButton" @click="actionButtonClicked" class="md-icon-button cl-action-button">
          <md-icon>notifications</md-icon>
        </md-button>
        <md-button v-if="menuButton" class="md-icon-button" :class="[actionButton ? 'cl-menu-button' : 'cl-menu-button-only']">
          <md-icon>more_vert</md-icon>
        </md-button>
      </div>
      <div class="md-toolbar-row" :class="[Boolean(subTitle) ? 'cl-second-row' : 'cl-second-row-off']">
        <div class="md-title cl-second-row-title">{{subTitle}}</div>
      </div>
    </md-toolbar>
    <div :class="[Boolean(subTitle) ? 'cl-fake-placeholder-header-big' : 'cl-fake-placeholder-header-small']"></div>
  </div>
</template>

<script lang="ts">
import {
  Component, Prop, Vue,
} from 'vue-property-decorator';

@Component({ components: {} })
export default class HeaderAttributes extends Vue {
  @Prop({
    required: true,
    default: 'DEV: Please add a title',
  })
  title!: string;

  @Prop()
  subTitle!: string;

  @Prop({ default: false })
  backButton!: boolean;

  @Prop({ default: false })
  actionButton!: boolean;

  @Prop({ default: false })
  menuButton!: boolean;

  actionButtonClicked() {
    this.$emit('actionButtonClicked');
  }
}
</script>

<style lang="scss">
  .cl-fake-placeholder-header-big {
    height: 136px;
    background-color: white;
  }
  .cl-fake-placeholder-header-small {
    height: 88px;
    background-color: white;
  }
  .cl-header {
    display: flex;
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: auto;
    padding-left: 24px;
    padding-top: 32px;
    z-index: 100;
  }
  .cl-first-row {
    display: flex;
    overflow-y: auto;
    width: inherit;
    overflow: inherit;
  }
  .cl-back-button {
    margin: -8px 0px -8px -16px !important;
  }
  .cl-action-button {
    margin: -8px 16px -8px auto !important;
    float: right;
  }
  .cl-menu-button {
     margin: -8px 8px -8px -8px  !important;
     float: right;
  }
  .cl-menu-button-only {
    margin: -8px 8px -8px auto  !important;
    float: right;
  }
  .cl-first-row-title {
    font-size: 24px;
    line-height: 24px;
    margin-left: 0              !important;
  }
  .cl-second-row {
    padding-top: 24px           !important;
    padding-bottom: 32px        !important;
  }
  .cl-second-row-off {
    height: 32px;
    min-height: 0;
  }
  .cl-second-row-title {
    font-size: 16px;
    line-height: 24px;
    margin-left: 0              !important;
  }
</style>
