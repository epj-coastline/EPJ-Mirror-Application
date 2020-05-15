<template>
  <div class="cl-study-group-list">
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
  import { StudyGroup } from '@/services/StudyGroup';
  import moment from 'moment-timezone';

  @Component({
    components: {
      ProfileImage,
    },
    methods: {
      formatDate(date: Date): string {
        const dateTimezoneZero = moment.tz(date, 'Africa/Dakar');
        return moment(dateTimezoneZero).fromNow();
      },
    },
  })
  export default class StudyGroupList extends Vue {
    @Prop()
    studyGroups!: Array<StudyGroup>;
  }


</script>

<style scoped lang="scss">
  .cl-study-group-list .md-list {
    padding: 20px 24px 20px 24px;
  }
  .cl-study-group-list .md-card {
    width: 100%;
    max-width: 400px;
    margin: 8px 0px;
    display: inline-block;
    vertical-align: top;
    border-radius: 4px;
    border: 1px solid rgba(0, 0, 0, 0.12);
    box-sizing: border-box;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.2), 0px 2px 1px rgba(0, 0, 0, 0.12), 0px 1px 1px rgba(0, 0, 0, 0.14);
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
  .cl-study-group-list .md-subhead, .md-card-content  {
    font-weight: 400 !important;
  }
</style>
