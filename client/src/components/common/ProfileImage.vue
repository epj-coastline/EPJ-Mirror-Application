<template>
  <md-avatar class="cl-profile-image md-avatar-icon md-primary">{{ initials }}</md-avatar>
</template>

<script lang="ts">
  import { Component, Prop, Vue } from 'vue-property-decorator';

  @Component
  export default class ProfileImage extends Vue {
    @Prop()
    firstName!: string;

    @Prop()
    lastName!: string;

    get initials() {
      return this.firstName.slice(0, 1).toUpperCase() + this.lastName.slice(0, 1).toUpperCase();
    }
  }
</script>

<style lang="scss">
  .cl-profile-image {
    font-size: 18px;
  }
</style>
