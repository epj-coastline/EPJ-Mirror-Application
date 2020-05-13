<template>
  <div class="cl-dialog">
    <form class="cl-form" novalidate @submit.prevent="validatePurpose">
        <h3 class="md-title cl-dialog-header">Neue Lerngruppe für {{moduleTitle}}</h3>
        <md-button class="md-icon-button cl-dialog-header cl-button-clear" @click="closeDialog" :disabled="sending">
          <md-icon>clear</md-icon>
        </md-button>

      <md-field :class="getValidationClass()" class="cl-field">
        <md-textarea class="cl-textarea" name="purpose" id="purpose" v-model="form.purpose" placeholder="Was ist der Zweck deiner Lerngruppe?" :disabled="sending" maxlength="140" required></md-textarea>
      </md-field>
      <md-progress-bar class="cl-progress-bar" md-mode="indeterminate" v-if="sending" />
      <md-dialog-actions class="cl-dialog-actions">
        <md-button class="md-raised md-primary cl-button-submit" type="submit" :disabled="isDisable(this.form.purpose)">Lerngruppe erstellen</md-button>
      </md-dialog-actions>
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
  import StudyGroupService from '@/services/studyGroupService';

@Component({
  mixins: [validationMixin],
  validations: {
    form: {
      purpose: {
        maxLength: maxLength(140),
        required,
      },
    },
  },
})

  export default class StudyGroupCreateDialog extends Vue {
    @Prop({
      required: true,
    })
    moduleId!: number;

    @Prop({
      required: true,
    })
    moduleTitle!: string;

    private form = { purpose: null };

    private sending = false;

    private invalidPurpose = true;

    getValidationClass() {
        const field = this.$v.form.purpose;
        this.invalidPurpose = !field || (field.$invalid && field.$dirty);
      }

      clearForm() {
        this.$v.$reset();
        this.form.purpose = null;
      }

      async saveStudyGroup() {
        this.sending = true;
        const purposeTmp = this.form.purpose;
        if (purposeTmp !== null) {
          await StudyGroupService.postStudyGroup(purposeTmp, this.moduleId);
          // Instead of this timeout, here you can call your API
          window.setTimeout(() => {
            this.sending = false;
            this.clearForm();
            this.$emit('closeCreateDialog');
          }, 1500);
        }
      }

      validatePurpose() {
        this.$v.$touch();

        if (!this.$v.$invalid) {
          this.saveStudyGroup();
        }
      }

      isDisable(purpose: any) {
        return this.sending || !purpose;
      }

      closeDialog() {
        this.clearForm();
        this.$emit('closeCreateDialog');
      }
  }


</script>

<style lang="scss" scoped>
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
    margin: 24px;
    max-width: 400px;
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
  .cl-textarea{
    height: 140px;
  }

</style>
