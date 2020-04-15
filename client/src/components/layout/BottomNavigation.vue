<template>
  <div>
    <md-bottom-bar class="bottom-navigation" md-sync-route>
      <md-bottom-bar-item to="/studygroups" md-label="Lerngruppen" md-icon="group"></md-bottom-bar-item>
      <md-bottom-bar-item to="/coaching" md-label="Nachhilfe" md-icon="school"></md-bottom-bar-item>
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
