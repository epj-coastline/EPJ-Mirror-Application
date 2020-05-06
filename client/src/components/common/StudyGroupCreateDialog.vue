<template>
  <div class="cl-dialog">
    <form class="cl-form" novalidate @submit.prevent="validatePurpose">
        <h3 class="md-title cl-dialog-header">Neue Lerngruppe für MODULTITEL</h3>
        <md-button class="md-icon-button cl-dialog-header cl-button-clear" @click="closeDialog" :disabled="sending">
          <md-icon>clear</md-icon>
        </md-button>

      <md-field :class="getValidationClass()" class="cl-field">
        <md-textarea name="purpose" id="purpose" v-model="form.purpose" placeholder="Was ist der Zweck deiner Lerngruppe?" :disabled="sending" maxlength="140" required></md-textarea>
        <span class="md-error">Bitte gib eine Beschreibung für deine Lerngruppe an</span>
      </md-field>
      <md-progress-bar class="cl-progress-bar" md-mode="indeterminate" v-if="sending" />
      <md-dialog-actions class="cl-dialog-actions">
        <md-button class="md-raised md-primary cl-button-submit" type="submit" :disabled="isDisable(this.form.purpose)">Lerngruppe erstellen</md-button>
      </md-dialog-actions>
    </form>
    <md-snackbar :md-active.sync="studyGroupSaved">Die Lerngruppe {{ lastStudyGroup }} wurde erstellt</md-snackbar>
  </div>
</template>

<script>
  import { validationMixin } from 'vuelidate';
  import {
    maxLength,
    required,
  } from 'vuelidate/lib/validators';
  import StudyGroupService from '@/services/studyGroupService';

  export default {
    name: 'StudyGroupCreateDialog',
    mixins: [validationMixin],
    data: () => ({
      form: {
        purpose: null,
      },
      studyGroupSaved: false,
      sending: false,
      invalidPurpose: true,
      lastStudyGroup: null,
      showDialog: false,
    }),
    validations: {
      form: {
        purpose: {
          maxLength: maxLength(140),
          required,
        },
      },
    },
    methods: {
      getValidationClass() {
        const field = this.$v.form.purpose;
        let returnObject = {};
        if (field) {
          returnObject = {
            'md-invalid': field.$invalid && field.$dirty,
          };
          this.invalidPurpose = field.$invalid && field.$dirty;
        }
        return returnObject;
      },
      clearForm() {
        this.$v.$reset();
        this.form.purpose = null;
      },
      saveStudyGroup() {
        this.sending = true;
        StudyGroupService.postStudyGroup(this.form.purpose, 0, 0);

        // Instead of this timeout, here you can call your API
        window.setTimeout(() => {
          this.lastStudyGroup = this.form.purpose;
          this.studyGroupSaved = true;
          this.sending = false;

          this.clearForm();
          this.$emit('close');
        }, 1500);
      },
      validatePurpose() {
        this.$v.$touch();

        if (!this.$v.$invalid) {
          this.saveStudyGroup();
        }
      },
      isDisable(purpose) {
        return this.sending || !purpose;
      },
      closeDialog() {
        this.clearForm();
        this.$emit('close');
      },
    },
  };

</script>

<style lang="scss" scoped>
  .md-dialog ::v-deep .md-dialog-container {
    max-width: 768px;
  }
  .cl-button-submit {
    width: 100%;
  }
  .cl-dialog-actions{
    padding-left: 0px;
    padding-right: 0px;
  }
  .cl-dialog {
    height: 100%;
    width: 100%;
    background-color: white;
    z-index: 1000;
    position: fixed;
    top: 0;
  }
  .cl-form {
    margin-top: 32px;
    margin-right: 16px;
    margin: 24px;
    margin-right: 24px;
  }
  .cl-field {
    margin-top: 35px;
    margin-bottom: 16px;
    width: initial;
  }
  .cl-button-clear{
    position: absolute;
    right: 8px;
    top: 16px;
  }
</style>
