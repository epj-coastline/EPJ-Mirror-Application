<template>
  <div>
    <md-bottom-bar class="bottom-navigation" md-sync-route>
      <md-bottom-bar-item to="/" exact md-label="Home" md-icon="home"></md-bottom-bar-item>
      <md-bottom-bar-item to="/students" md-label="Studierende" md-icon="people"></md-bottom-bar-item>
    </md-bottom-bar>
    <div class="cl-fake-placeholder-bottom-bar">
    </div>
  </div>
</template>

<style lang="scss">
  .bottom-navigation {
    position: fixed;
    bottom: 0;
    z-index: 100;
  }
  .cl-fake-placeholder-bottom-bar {
    height: 56px;
    width: 100%;
    background-color: white;
    display: flex;
  }
</style>
