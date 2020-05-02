<template>
  <md-list class="md-double-line cl-user-list">
    <md-subheader class="cl-subheader">{{ title}}</md-subheader>
    <div v-for="user in users" :key="user.id">
      <md-list-item>
        <profile-image v-bind:first-name="user.firstName" v-bind:last-name="user.lastName" />
        <div class="md-list-item-text">
          <span>{{ `${user.firstName} ${user.lastName}` }} </span>
          <span>{{ user[secondRowContent] }}</span>
        </div>
      </md-list-item>
      <md-divider class="md-inset"></md-divider>
    </div>
  </md-list>
</template>

<script lang="ts">
  import { Component, Prop, Vue } from 'vue-property-decorator';
  import { User } from '@/services/User';
  import ProfileImage from '@/components/common/ProfileImage.vue';

  @Component({ components: { ProfileImage } })
  export default class UserList extends Vue {
    @Prop()
    users!: Array<User>;

    @Prop()
    title!: string;

    @Prop({ default: 'degreeProgram' })
    secondRowContent!: string;
  }
</script>

<style scoped lang="scss">
  .cl-user-list .md-list-item-text > span:first-child{
    margin-bottom: 2px;
  }
   .cl-subheader {
    min-height: 24px;
    color: rgba(0, 0, 0, 0.87);
  }
</style>
