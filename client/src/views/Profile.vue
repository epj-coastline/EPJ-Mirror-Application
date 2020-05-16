<template>
  <div>
    <Header :title=titleName :sub-title="email" menu-button="true"/>
    <EditUserData v-if="dataIsLoaded" :user="user" v-on:userUpdated="loadData"/>
    <LoadingSpinner v-if="!dataIsLoaded"/>
  </div>
</template>

<style scoped>

</style>

<script lang="ts">
  import {
    Component, Vue, Watch,
  } from 'vue-property-decorator';
  import Header from '@/components/layout/Header.vue';
  import { getAuthService } from '@/auth/authServiceFactory';
  import EditUserData from '@/components/common/EditUserData.vue';
  import UserService from '@/services/userService';
  import { User } from '@/services/User';
  import LoadingSpinner from '@/components/common/LoadingSpinner.vue';


  @Component({
    components: {
      EditUserData,
      Header,
      LoadingSpinner,
    },
  })
  export default class Profile extends Vue {
    private authService = getAuthService();

    private email?: string = this.authService.user.email;

    private user?: User = undefined;

    private titleName = 'Profil lädt...';

    private dataIsLoaded = false;

    @Watch('$route', { immediate: true, deep: true })
    async loadData() {
      try {
        if (this.user === undefined) {
          this.user = await UserService.getMyUser();
        }
        this.titleName = `${this.user.firstName} ${this.user.lastName}`;
      } catch {
        await this.$router.push('/studygroups');
      } finally {
        this.dataIsLoaded = true;
      }
    }
  }
</script>
