<template>
  <div class="coastline-app">
    <Header/>
    <main>
      <router-view/>
    </main>
    <BottomNavigation/>
  </div>
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

  .coastline-app {
    height: calc(100vh - 56px);
  }

  main {
    background-color: white;
    min-height: 100%;
    padding: 16px;
  }
</style>

<script>
import BottomNavigation from '@/components/BottomNavigation.vue';
import Header from '@/components/Header.vue';

export default {
  components: {
    BottomNavigation,
    Header,
  },
};
</script>
