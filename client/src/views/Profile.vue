<template>
  <div>
    <Header :title="user.nickname" :sub-title="user.email" menu-button="true"/>
  </div>
</template>

<style scoped>

</style>

<script lang="ts">
  import {
    Component, Vue,
  } from 'vue-property-decorator';
  import Header from '@/components/layout/Header.vue';
  import { Auth0User } from '@/auth/interfaces';
  import { getAuthService } from '@/auth/authServiceFactory';


  @Component({
    components: {
      Header,
    },
  })
  export default class Profile extends Vue {
    private authService = getAuthService();

    private user?: Auth0User = this.authService.user;
  }
</script>
