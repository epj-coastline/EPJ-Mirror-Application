<template>
  <div>
    <md-list>
      <div v-for="group in studyGroups" v-bind:key="group.id">
        <md-card>
          <md-card-header>
            <profile-image v-bind:first-name="group.user.firstName" v-bind:last-name="group.user.lastName" />
            <div class="cl-headline6">{{ `${group.user.firstName} ${group.user.lastName}` }} </div>
            <div class="md-subhead">{{formatDate(group.creationDate)}}</div>
          </md-card-header>
          <md-card-content class="md-body-2">
            {{group.purpose}}
          </md-card-content>
        </md-card>
      </div>
    </md-list>
  </div>
</template>

<script lang="ts">
  import {
    Component, Prop, Vue,
  } from 'vue-property-decorator';
  import ProfileImage from '@/components/common/ProfileImage.vue';
  import StudyGroup from '@/services/StudyGroup';
  import moment from 'moment';

  @Component({
    components: {
      ProfileImage,
    },
    methods: {
      formatDate(date: Date): string {
          return moment(date, 'YYYY-MM-DD').fromNow();
      },
    },
  })
  export default class StudyGroupList extends Vue {
    @Prop()
    studyGroups!: Array<StudyGroup>;
  }


</script>

<style scoped lang="scss">
  .md-list {
    padding: 20px 24px 20px 24px;
  }
  .md-card {
    width: 100%;
    max-width: 400px;
    margin: 8px 0px;
    display: inline-block;
    vertical-align: top;
  }
  .cl-headline6 {
    font-family: Roboto;
    font-style: normal;
    font-weight: 500;
    font-size: 20px;
    line-height: 24px;
    letter-spacing: 0.15px;
    color: rgba(0, 0, 0, 0.87);
  }
</style>
