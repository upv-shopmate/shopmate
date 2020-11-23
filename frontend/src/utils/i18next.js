import i18n from 'i18next';
import {initReactI18next} from 'react-i18next';
import es from '../assets/locale/es/common.json';
import en from '../assets/locale/en/common.json';
import ca from '../assets/locale/ca/common.json';
// import Backend from 'i18next-http-backend';
import LanguageDetector from 'i18next-browser-languagedetector';
// don't want to use this?
// have a look at the Quick start guide
// for passing in lng and translations on init

const Languages = ['es', 'en', 'ca'];

i18n
// load translation using http -> see /public/locales (i.e. https://github.com/i18next/react-i18next/tree/master/example/react/public/locales)
// learn more: https://github.com/i18next/i18next-http-backend
// .use(Backend)
// detect user language
// learn more: https://github.com/i18next/i18next-browser-languageDetector
    .use(LanguageDetector)
// pass the i18n instance to react-i18next.
    .use(initReactI18next)
// init i18next
// for all options read: https://www.i18next.com/overview/configuration-options
    .init({
      fallbackLng: 'es',
      debug: true,
      whitelist: Languages,
      react: {
        useSuspense: false,
        wait: true,
      },
      initInmediate: false,
      nonExplicitWhitelist: true,
      lowerCaseLng: true,
      load: 'all',
      parseMissingKeyHandler: (key) => `[[ ${key} ]]`,
      appendNamespaceToMissingKey: true,
      defaultNS: 'common',
      ns: ['common'],
      lng: 'es',
      resources: {
        es: {
          common: es,
        },
        en: {
          common: en,
        },
        ca: {
          common: ca,
        },
      },

      interpolation: {
        escapeValue: false, // not needed for react as it escapes by default
      },
    });


export default i18n;
