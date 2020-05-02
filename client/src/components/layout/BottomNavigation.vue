<template>
  <div>
    <md-bottom-bar class="cl-bottom-navigation" md-sync-route>
      <md-bottom-bar-item to="/studygroups" md-label="Lerngruppen" md-icon="group"></md-bottom-bar-item>
      <md-bottom-bar-item to="/coaching" md-label="Nachhilfe" md-icon="school"></md-bottom-bar-item>
      <md-bottom-bar-item to="/profile" md-label="Profil" md-icon="account_circle"></md-bottom-bar-item>
    </md-bottom-bar>
    <div class="cl-placeholder-bottom-bar">
    </div>
  </div>
</template>

<style lang="scss">
  .cl-bottom-navigation {
    position: fixed;
    bottom: 0;
    z-index: 100;
  }
  .cl-placeholder-bottom-bar {
    height: 56px;
    width: 100%;
    background-color: white;
    display: flex;
  }
</style>
