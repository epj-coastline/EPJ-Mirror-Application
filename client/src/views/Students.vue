<template>
  <div>
    <Header title="Studierende"/>
    <UserList :users="students" title="Studierende"/>
  </div>
</template>

<script lang="ts">
  import {
    Component, Watch, Vue,
  } from 'vue-property-decorator';
  import UserService from '@/services/userService';
  import User from '@/services/User';
  import UserList from '@/components/common/UserList.vue';
  import Header from '@/components/layout/Header.vue';

  @Component({
    components: {
      UserList,
      Header,
    },

  })
  export default class Student extends Vue {
    private students: Array<User> = [];

    @Watch('$route', { immediate: true, deep: true })
    async loadList() {
      this.students = await UserService.getAll();
    }
  }
</script>
