<template>
  <div>
    <div>
      <md-button v-on:click="loadList" class="md-raised md-primary">Load list</md-button>
    </div>
    <md-list class="md-triple-line">
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
export default {
  name: 'TripleLine',
  data() {
    return {
      testList: [],
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
    loadList() {
      // TODO: use API URL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      fetch('https://yoloo.free.beeceptor.com', {
        method: 'GET',
      })
        .then((response) => response.json())
        .then((result) => { this.testList = result; });
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
