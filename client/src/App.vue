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

  body {
    background-color: white;
  }

  main {
    background-color: white;
    padding-top: 80px;
    padding-bottom:  56px;
  }
</style>

<script>
import BottomNavigation from '@/components/layout/BottomNavigation.vue';
import Header from '@/components/layout/Header.vue';

export default {
  components: {
    BottomNavigation,
    Header,
  },
};
</script>
