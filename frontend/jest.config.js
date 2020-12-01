module.exports = {
    collectCoverageFrom: ["src/**/*.{js,jsx,mjs}"],
    testMatch: ["<rootDir>/**/__tests__/**/*.{js,jsx,mjs}", "<rootDir>/src/**/?(*.)(spec|test).{js,jsx,mjs}"],
    moduleNameMapper: {
      '\\.(css|less|jpg|png|gif)$': '<rootDir>/__mocks__/styleMock.js',
    },
  };
  