<template>
  <md-list class="md-double-line">
    <md-subheader class="subheader">{{ title}}</md-subheader>
    <div v-for="user in users" :key="user.id">
      <md-list-item :to="{ name: 'Student', params: { userId: 123 }}" exact>
        <profile-image v-bind:first-name="user.firstName" v-bind:last-name="user.lastName" />
        <div class="md-list-item-text">
          <span>{{ `${user.firstName} ${user.lastName}` }} </span>
          <span>{{ user.degreeProgram }}</span>
        </div>
      </md-list-item>
      <md-divider class="md-inset"></md-divider>
    </div>
  </md-list>
</template>

<script lang="ts">
  import { Component, Prop, Vue } from 'vue-property-decorator';
  import User from '@/services/User';
  import ProfileImage from '@/components/common/ProfileImage.vue';

  @Component({ components: { ProfileImage } })
  export default class UserList extends Vue {
    @Prop()
    users!: Array<User>;

    @Prop()
    title!: string;
  }
</script>

<style scoped lang="scss">
  .md-list-item-text > span:first-child{
    margin-bottom: 2px;
  }
  .subheader {
    min-height: 24px;
    color: rgba(0, 0, 0, 0.87);
  }
</style>
