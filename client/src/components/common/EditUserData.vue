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
                <span class="md-error" v-if="!$v.form.firstName.required">Der Vorname wird benötigt</span>
                <span class="md-error" v-else-if="!$v.form.firstName.maxlength">Der Vorname ist zu lang</span>
              </md-field>
            </div>
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('lastName')">
                <label for="last-name">Nachname</label>
                <md-input name="last-name" id="last-name" autocomplete="family-name" v-model="form.lastName" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.lastName.maxlength">Der Nachname ist zu lang</span>
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
                  <span class="md-error">Der Studiengang wird benötigt</span>
                </md-field>
              </div>
            </div>
            <div class="md-layout md-gutter">
              <div class="md-layout-item md-small-size-100">
                <md-field :class="getValidationClass('startDate')">
                  <label for="startDate">Start</label>
                  <md-select name="startDate" id="startDate" v-model="form.startDate" md-dense :disabled="sending">
                    <md-option value="FS16">FS16</md-option>
                    <md-option value="HS16">HS16</md-option>
                    <md-option value="FS17">FS17</md-option>
                    <md-option value="HS17">HS17</md-option>
                    <md-option value="FS18">FS18</md-option>
                    <md-option value="HS18">HS18</md-option>
                    <md-option value="FS19">FS19</md-option>
                    <md-option value="HS19">HS19</md-option>
                    <md-option value="FS20">FS20</md-option>
                  </md-select>
                  <span class="md-error">Das Startdatum wird benötigt</span>
                </md-field>
              </div>
            </div>
          </md-card-content>
          <md-progress-bar md-mode="indeterminate" v-if="sending" />
          <md-button type="submit" class="md-primary md-raised cl-wide-button" :disabled="sending">Speichern</md-button>
        </md-card>
     </form>
    </div>
  </template>

  <script lang="ts">
  import { validationMixin } from 'vuelidate';
  import { required, maxLength } from 'vuelidate/lib/validators';
  import { Component, Vue, Prop } from 'vue-property-decorator';
  import { User } from '@/services/User';
  import UserService from '@/services/userService';


  @Component({
    mixins: [validationMixin],
    validations: {
      form: {
        firstName: {
          required,
          maxLength: maxLength(20),
        },
        lastName: {
          maxLength: maxLength(20),
        },
        startDate: {
          required,
        },
        degreeProgram: {
          required,
        },
      },
    },
  })
  export default class EditUserData extends Vue {
      @Prop({
        required: true,
      })
      user!: User;

      private form = this.user;

      private sending = false;

      getValidationClass(fieldName: string) {
        const field = this.$v.form[fieldName];
        let returnObject = {};
          if (field) {
            returnObject = {
              'md-invalid': field.$invalid && field.$dirty,
            };
          }
        return returnObject;
      }

      saveUser() {
        this.sending = true;

        const dataObject = {
          firstName: this.form.firstName,
          lastName: this.form.lastName,
          degreeProgram: this.form.degreeProgram,
          startDate: this.form.startDate,
        };

        const requestUpdateUser = UserService.updateUser(dataObject);
        const wait = new Promise((resolve) => {
          setTimeout(resolve, 1500);
        });

        Promise.all([requestUpdateUser, wait]).then(() => {
          this.sending = false;
          this.userUpdated();
        }).catch(() => {
          this.sending = false;
          this.$router.push('/studygroups');
        });
      }

      validateUser() {
        this.$v.$touch();

        if (!this.$v.$invalid) {
          this.saveUser();
        }
      }

      userUpdated() {
        this.$emit('userUpdated');
      }
  }
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
