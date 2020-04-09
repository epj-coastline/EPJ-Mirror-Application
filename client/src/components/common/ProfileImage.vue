<template>
  <md-avatar class="profile-image md-avatar-icon md-primary">{{ initials }}</md-avatar>
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
      return this.firstName.slice(0, 1) + this.lastName.slice(0, 1);
    }
  }
</script>

<style lang="scss">
  .profile-image {
    font-size: 18px;
  }
</style>
