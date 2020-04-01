<template>
  <div>
    <form novalidate class="md-layout" @submit.prevent="validateUser">
      <md-card class="md-layout-item md-size-50 md-small-size-100">
        <md-card-header>
          <div class="md-title">Create User</div>
        </md-card-header>

        <md-card-content>
          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('firstName')">
                <label for="first-name">First Name</label>
                <md-input name="first-name" id="first-name" autocomplete="given-name" v-model="form.firstName" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.firstName.required">The first name is required</span>
                <span class="md-error" v-else-if="!$v.form.firstName.minlength">Invalid first name</span>
              </md-field>
            </div>

            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('lastName')">
                <label for="last-name">Last Name</label>
                <md-input name="last-name" id="last-name" autocomplete="family-name" v-model="form.lastName" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.lastName.required">The last name is required</span>
                <span class="md-error" v-else-if="!$v.form.lastName.minlength">Invalid last name</span>
              </md-field>
            </div>
          </div>

          <md-field :class="getValidationClass('email')">
            <label for="email">Email</label>
            <md-input type="email" name="email" id="email" autocomplete="email" v-model="form.email" :disabled="sending" />
            <span class="md-error" v-if="!$v.form.email.required">The email is required</span>
            <span class="md-error" v-else-if="!$v.form.email.email">Invalid email</span>
          </md-field>

          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('degreeprogram')">
                <label for="degreeprogram">Studiengang</label>
                <md-select name="degreeprogram" id="degreeprogram" v-model="form.degreeprogram" md-dense :disabled="sending">
                  <md-option></md-option>
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
              <md-field :class="getValidationClass('startdate')">
                <label for="startdate">Start</label>
                <md-select name="startdate" id="startdate" v-model="form.startdate" md-dense :disabled="sending">
                  <md-option></md-option>
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
          <md-field>
            <label>Bio</label>
            <md-textarea v-model="form.biography" maxlength="140"></md-textarea>
          </md-field>
        </md-card-content>

        <md-progress-bar md-mode="indeterminate" v-if="sending" />

        <md-card-actions>
          <md-button type="submit" class="md-primary" :disabled="sending">Create user</md-button>
        </md-card-actions>
      </md-card>

      <md-snackbar :md-active.sync="userSaved">The user {{ lastUser }} was saved with success!</md-snackbar>
    </form>
  </div>
</template>

<script>
import { validationMixin } from 'vuelidate';
import {
  required,
  email,
  minLength,
  maxLength,
} from 'vuelidate/lib/validators';

export default {
  name: 'FormValidation',
  mixins: [validationMixin],
  data: () => ({
    form: {
      firstName: null,
      lastName: null,
      email: null,
      degreeprogram: null,
      startdate: null,
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
      startdate: {
        required,
      },
      degreeprogram: {
        required,
      },
      biography: {
        maxLength: maxLength(140),
      },
      email: {
        required,
        email,
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
      this.form.email = null;
      this.form.biography = null;
      this.form.degreeprogram = null;
      this.form.startdate = null;
    },
    saveUser() {
      this.sending = true;
      const dataObject = {
        firstname: this.form.firstName,
        lastname: this.form.lastName,
        email: this.form.email,
        biography: this.form.biography,
        degreeprogram: this.form.degreeprogram,
        startdate: this.form.startdate,
      };
      // TODO: use API URL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      fetch('https://yoloo.free.beeceptor.com', {
        method: 'POST',
        headers: {
          'content-type': 'application/json',
        },
        body: JSON.stringify(dataObject),
      });
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
};
</script>

<style lang="scss" scoped>
  .md-progress-bar {
    position: absolute;
    top: 0;
    right: 0;
    left: 0;
  }
</style>
