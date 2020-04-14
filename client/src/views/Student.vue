<template>
  <div>
    <Header title="qwertzuiopasdfghjklyxcvbnmüöäèéäÖÉ" @actionButtonClicked="coolAlert" :backButton="true" :menu-button="true" :action-button="true" sub-title="qwertzuiopasdfghjklyxcvbnmüöäèéäÖÉ"/>
    <p>FIRST</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
    <p>dsfhbvhsjdbvsdfvs</p>
  </div>
</template>

<script lang="ts">
  import {
    Component, Prop, Vue,
  } from 'vue-property-decorator';
  import User from '@/services/User';
  import Header from '@/components/layout/Header.vue';

  @Component({
    components: {
      Header,
    },
    methods: {
      coolAlert() {
        // eslint-disable-next-line
        alert('I just clicked the action Button');
      },
    },
  })
  export default class StudentOne extends Vue {
    @Prop()
    student!: User;
  }
</script>
