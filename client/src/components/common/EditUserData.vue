<template>
  <div>
    <form novalidate class="md-layout cl-user-data-form" @submit.prevent="validateUser">
      <md-card class="md-layout-item md-size-50 md-small-size-100">
        <md-card-content>
          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('firstName')">
                <label for="first-name">Vorname</label>
                <md-input name="first-name" id="first-name" autocomplete="given-name" v-model="form.firstName" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.firstName.required">The first name is required</span>
                <span class="md-error" v-else-if="!$v.form.firstName.minlength">Invalid first name</span>
              </md-field>
            </div>

            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('lastName')">
                <label for="last-name">Nachname</label>
                <md-input name="last-name" id="last-name" autocomplete="family-name" v-model="form.lastName" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.lastName.required">The last name is required</span>
                <span class="md-error" v-else-if="!$v.form.lastName.minlength">Invalid last name</span>
              </md-field>
            </div>
          </div>

          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('degreeProgram')">
                <label for="degreeProgram">Studiengang</label>
                <md-select name="degreeProgram" id="degreeProgram" v-model="form.degreeProgram" md-dense :disabled="sending">
                  <md-option value="Bauingenieurwesen">Bauingenieurwesen</md-option>
                  <md-option value="Elektrotechnik">Elektrotechnik</md-option>
                  <md-option value="Erneuerbare Energien und Umwelttechnik">Erneuerbare Energien und Umwelttechnik</md-option>
                  <md-option value="Informatik">Informatik</md-option>
                  <md-option value="Landschaftsarchitektur">Landschaftsarchitektur</md-option>
                  <md-option value="Maschinentechnik | Innovation">Maschinentechnik | Innovation</md-option>
                  <md-option value="Stadt-, Verkehrs- und Raumplanung">Stadt-, Verkehrs- und Raumplanung</md-option>
                  <md-option value="Wirtschaftsingenieurwesen">Wirtschaftsingenieurwesen</md-option>
                </md-select>
                <span class="md-error">The degree program is required</span>
              </md-field>
            </div>
          </div>
          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('startDate')">
                <label for="startDate">Start</label>
                <md-select name="startDate" id="startDate" v-model="form.startDate" md-dense :disabled="sending">
                  <md-option value="FS2016">FS2016</md-option>
                  <md-option value="HS2016">HS2016</md-option>
                  <md-option value="FS2017">FS2017</md-option>
                  <md-option value="HS2017">HS2017</md-option>
                  <md-option value="FS2018">FS2018</md-option>
                  <md-option value="HS2018">HS2018</md-option>
                  <md-option value="FS2019">FS2019</md-option>
                  <md-option value="HS2019">HS2019</md-option>
                  <md-option value="FS2020">FS2020</md-option>
                </md-select>
                <span class="md-error">The start date is required</span>
              </md-field>
            </div>
          </div>
        </md-card-content>

        <md-progress-bar md-mode="indeterminate" v-if="sending" />

        <md-button type="submit" class="md-primary md-raised cl-wide-button" :disabled="sending">Speichern</md-button>
      </md-card>

      <md-snackbar :md-active.sync="userSaved">The user {{ lastUser }} was saved with success!</md-snackbar>
    </form>
  </div>
</template>

<script>
  import { validationMixin } from 'vuelidate';
  import {
    required,
    minLength,
    maxLength,
  } from 'vuelidate/lib/validators';
  import { Component, Vue } from 'vue-property-decorator';

  @Component({
    mixins: [validationMixin],
    data: () => ({
      form: {
        firstName: null,
        lastName: null,
        degreeProgram: null,
        startDate: null,
        biography: null,
      },
      userSaved: false,
      sending: false,
      lastUser: null,
    }),
    validations: {
      form: {
        firstName: {
          required,
          minLength: minLength(3),
        },
        lastName: {
          required,
          minLength: minLength(3),
        },
        startDate: {
          required,
        },
        degreeProgram: {
          required,
        },
        biography: {
          maxLength: maxLength(140),
        },
      },
    },
    methods: {
      getValidationClass(fieldName) {
        const field = this.$v.form[fieldName];
        let returnObject = {};
        if (field) {
          returnObject = {
            'md-invalid': field.$invalid && field.$dirty,
          };
        }
        return returnObject;
      },
      clearForm() {
        this.$v.$reset();
        this.form.firstName = null;
        this.form.lastName = null;
        this.form.biography = null;
        this.form.degreeProgram = null;
        this.form.startDate = null;
      },
      saveUser() {
        this.sending = true;

        /*
        const dataObject = {
          firstName: this.form.firstName,
          lastName: this.form.lastName,
          biography: this.form.biography,
          degreeProgram: this.form.degreeProgram,
          startDate: this.form.startDate,
        };
        fetch(`${Configuration.CONFIG.backendHost}/users`, {
          method: 'POST',
          headers: {
            'content-type': 'application/json',
          },
          body: JSON.stringify(dataObject),
        }); */

        // Instead of this timeout, here you can call your API
        window.setTimeout(() => {
          this.lastUser = `${this.form.firstName} ${this.form.lastName}`;
          this.userSaved = true;
          this.sending = false;

          this.clearForm();
        }, 1500);
      },
      validateUser() {
        this.$v.$touch();

        if (!this.$v.$invalid) {
          this.saveUser();
        }
      },
    },
  })
  export default class EditUserData extends Vue {}
</script>

<style lang="scss" scoped>
  .cl-user-data-form .md-card {
    box-shadow: none;
  }
  .cl-user-data-form .md-card-content {
    padding: 24px 24px 7px;
  }
  .cl-user-data-form .md-ripple {
    padding: 0 calc(50vw - 68px) !important;
  }
  .cl-wide-button {
    width: calc(100% - 48px);
    margin: 0 0 0 24px;
  }
  .cl-no-textarea-resize {
    resize: none !important;
  }
  .md-progress-bar {
    position: absolute;
    top: 0;
    right: 0;
    left: 0;
  }
</style>
