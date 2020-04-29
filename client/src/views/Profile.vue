<template>
  <div>
    <Header :title="user.nickname" :sub-title="user.email"/>
    <md-button style="margin-left: 24px; margin-top: 40px" v-on:click="authService.logout()" class="md-raised md-primary">Logout</md-button>
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
