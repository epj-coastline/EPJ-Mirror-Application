<template>
  <div>
    <Header title="Student bababababababa" @actionButtonClicked="coolAlert" :backButton="true" :menu-button="true" :action-button="true" sub-title="Very subtitle wow"/>
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
      alert('I just clicked the action Button');
    },
  },
})
export default class StudentOne extends Vue {
  @Prop()
  student!: User;
}
</script>
