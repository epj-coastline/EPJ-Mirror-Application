<template>
  <div class="cl-dialog">
    <md-progress-bar class="cl-progress-bar" md-mode="indeterminate" v-if="sending" />
    <form class="cl-form" novalidate @submit.prevent="validatePurpose">
      <h3 class="md-title cl-dialog-header">Neue Lerngruppe für {{moduleTitle}}</h3>
      <md-button class="md-icon-button cl-dialog-header cl-button-clear" @click="closeDialog" :disabled="sending">
        <md-icon>clear</md-icon>
      </md-button>
      <md-field :class="getValidationClass()" class="cl-field">
        <md-textarea  class="cl-textarea"
                      name="purpose"
                      id="purpose"
                      v-model="purpose"
                      placeholder="Was ist der Zweck deiner Lerngruppe?"
                      :disabled="sending"
                      maxlength="140"
                      required>
        </md-textarea>
      </md-field>
      <md-card-actions class="cl-dialog-actions">
        <md-button  class="md-raised md-primary cl-button-submit"
                    type="submit"
                    :disabled="isDisabled(this.purpose)">
          Lerngruppe erstellen
        </md-button>
      </md-card-actions>
    </form>
  </div>
</template>


<script lang="ts">
  import { validationMixin } from 'vuelidate';
  import {
    Component, Prop, Vue,
  } from 'vue-property-decorator';
  import {
    maxLength,
    required,
  } from 'vuelidate/lib/validators';
  import StudyGroupService from '@/services/study-group/StudyGroupService';

  @Component({
    mixins: [validationMixin],
    validations: {
      purpose: {
        maxLength: maxLength(140),
        required,
      },
    },
  })

  export default class StudyGroupCreateDialog extends Vue {
    @Prop({
      required: true,
    })
    moduleId!: string;

    @Prop({
      required: true,
    })
    moduleTitle!: string;

    private purpose = '';

    private sending = false;

    private invalidPurpose = true;

    saveStudyGroup() {
      this.sending = true;
      try {
        if (this.purpose.length > 0) {
          const requestPostStudyGroup = StudyGroupService.postStudyGroup(this.purpose, this.moduleId);
          const wait = new Promise((resolve) => {
            setTimeout(resolve, 1500);
          });

          Promise.all([requestPostStudyGroup, wait]).then(() => {
            this.sending = false;
            this.closeDialog();
          });
        } else {
          throw Error('The study group could not be created.');
        }
      } catch {
        this.closeDialog();
        this.$router.push('/studygroups');
      }
    }

    getValidationClass() {
      const field = this.$v.purpose;
      this.invalidPurpose = !field || (field.$invalid && field.$dirty);
    }

    validatePurpose() {
      this.$v.$touch();

      if (!this.$v.$invalid) {
        this.saveStudyGroup();
      }
    }

    isDisabled(purpose: string) {
      return this.sending || purpose.length === 0;
    }

    clearForm() {
      this.$v.$reset();
      this.purpose = '';
    }

    closeDialog() {
      this.clearForm();
      this.$emit('closeCreateDialog');
    }
  }
</script>

<style lang="scss" scoped>
  .cl-dialog {
    height: 100%;
    width: 100%;
    background-color: white;
    z-index: 1000;
    position: fixed;
    top: 0;
  }

  .cl-form {
    margin: 24px;
    max-width: 400px;
  }

  .cl-field {
    margin-top: 35px;
    margin-bottom: 16px;
    width: initial;
  }

  .cl-textarea {
    height: 140px;
  }

  .cl-dialog-actions {
    padding-left: 0;
    padding-right: 0;
  }

  .cl-button-submit {
    width: 100%;
  }

  .cl-button-clear {
    position: absolute;
    right: 8px;
    top: 16px;
  }
  .md-progress-bar {
    position: absolute;
    top: 0;
    right: 0;
    left: 0;
  }
</style>
