<template>
  <div id="app">
    <div class="page-container">
      <md-app md-waterfall md-mode="fixed">
        <md-app-toolbar class="md-primary" >
          <span class="md-title">Coastline Demo</span>
        </md-app-toolbar>
        <md-app-content>
          <router-view/>
        </md-app-content>
      </md-app>
    </div>
    <md-bottom-bar md-sync-route>
      <md-bottom-bar-item to="/" exact md-label="Home" md-icon="home"></md-bottom-bar-item>
      <md-bottom-bar-item to="/about" md-label="People" md-icon="people"></md-bottom-bar-item>
    </md-bottom-bar>
  </div>
</template>

<style lang="scss">
  #app {
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
  }

  .md-app {
    border: 1px solid rgba(#000, .12);
    height: calc(100vh - 56px);
  }

  .md-bottom-bar {
    position: fixed;
    bottom: 0;
  }
</style>

<script>
export default {
  name: 'AppBody',
};
</script>
