<template>
  <div>
    <md-progress-spinner class="md-accent" md-mode="indeterminate"></md-progress-spinner>
    <main class="not-visible" id="main">
      <router-view/>
      <BottomNavigation/>
    </main>
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

  .not-visible {
      opacity: 0;
  }
</style>
<script>
  import BottomNavigation from '@/components/layout/BottomNavigation.vue';

  export default {
    components: {
      BottomNavigation,
    },
  };
</script>
