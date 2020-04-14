<template>
  <div>
    <md-bottom-bar class="bottom-navigation" md-sync-route>
      <md-bottom-bar-item to="/studygroups" exact md-label="Lerngruppen" md-icon="group"></md-bottom-bar-item>
      <md-bottom-bar-item to="/coaching" exact md-label="Nachhilfe" md-icon="school"></md-bottom-bar-item>
      <md-bottom-bar-item to="/" exact md-label="UserDemo1" md-icon="person_add"></md-bottom-bar-item>
      <md-bottom-bar-item to="/students" md-label="UserDemo2" md-icon="list"></md-bottom-bar-item>
      <md-bottom-bar-item to="/experimental" exact md-label="Testing" md-icon="new_releases"></md-bottom-bar-item>
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
