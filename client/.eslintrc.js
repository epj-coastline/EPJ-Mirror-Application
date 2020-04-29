module.exports = {
  parser: 'vue-eslint-parser',
  root: true,
  env: {
    node: true,
  },
  extends: [
    'plugin:vue/essential',
    '@vue/airbnb',
    '@vue/typescript/recommended',
  ],
  parserOptions: {
    ecmaVersion: 2020,
    parser: '@typescript-eslint/parser',
  },
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'linebreak-style': ['error', 'unix'],
    indent: 'off',
    'vue/script-indent': ['error', 2, { baseIndent: 1 }],
  },
  overrides: [
    {
      files: [
        '**/__tests__/*.{j,t}s?(x)',
        '**/tests/unit/**/*.spec.{j,t}s?(x)',
        '*.vue',
        '*.ts',
      ],
      rules: {
        'max-len': 'off', // disables line length check
        'no-underscore-dangle': 'off',
      },
      env: {
        mocha: true,
      },
    },
  ],
};
