<template>
  <div>
    <div>
      <md-button v-on:click="loadList" class="md-raised md-primary">Load list</md-button>
    </div>
    <md-list class="md-triple-line">
      <md-subheader>{{ `Running on Host: ${this.backendHost}` }}</md-subheader>
      <div v-for="item in testList" :key="item.firstname">
        <md-list-item >
          <md-avatar class="md-avatar-icon md-primary">{{ item.firstname.toString().slice(0,1) + item.lastname.toString().slice(0,1) }}</md-avatar>

          <div class="md-list-item-text">
            <span>{{ item.firstname + ' ' + item.lastname }}</span>
            <span>{{ item.degreeprogram  }}</span>
            <span>{{ item.startdate  }}</span>
          </div>
        </md-list-item>
        <md-divider class="md-inset"></md-divider>
      </div>
    </md-list>
  </div>
</template>

<script>
import UserService from '../services/userService';
import Configuration from '../Configuration';

export default {
  name: 'TripleLine',
  data() {
    return {
      testList: [],
      backendHost: Configuration.CONFIG.backendHost,
    };
  },
  created() {
    // fetch the data when the view is created and the data is
    // already being observed
    this.loadList();
  },
  watch: {
    // call again the method if the route changes
    $route: 'loadList',
  },
  methods: {
    async loadList() {
      this.testList = await UserService.loadList();
    },
  },
};
</script>

<style lang="scss" scoped>
  .md-list {
    width: 100%;
    max-width: 100%;
    display: inline-block;
    vertical-align: top;
    border: 1px solid rgba(#000, .12);
  }
</style>
