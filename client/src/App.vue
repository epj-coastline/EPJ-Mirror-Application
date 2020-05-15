<template>
  <main v-bind:class="{ 'not-visible': showContent }">
    <router-view/>
    <BottomNavigation/>
  </main>
</template>

<style lang="scss">
  // Import the theme engine
  @import "~vue-material/dist/theme/engine";

  // Define primary and secondary color
  @include md-register-theme("default", (
    primary: md-get-palette-color(indigo, 600),
    accent: md-get-palette-color(indigo, 600)
  ));

  // Apply the theme
  @import "~vue-material/dist/theme/all";

  body {
    background-color: white;
  }

  .not-visible {
    opacity: 0;
  }

  // Global material overwrites
  .md-select-menu {
    left: 24px!important;
    min-width: calc(100vw - 48px)!important;
  }

</style>
<script lang="ts">
  import BottomNavigation from '@/components/layout/BottomNavigation.vue';
  import { Component, Vue } from 'vue-property-decorator';
  import { getAuthService } from '@/auth/authServiceFactory';


  @Component({
    components: {
      BottomNavigation,
    },
  })
  export default class App extends Vue {
    private authService = getAuthService();

    get showContent() {
      return this.authService.isLoading || !this.authService.isAuthenticated;
    }
  }
</script>
