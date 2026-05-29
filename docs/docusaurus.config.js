// @ts-check
const {themes: prismThemes} = require('prism-react-renderer');

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'NPC Mentality',
  tagline: 'Unity NPC AI Package',
  favicon: 'img/favicon.ico',
  url: 'https://achieveonepark.github.io',
  baseUrl: '/npc-mentality/',
  organizationName: 'achieveonepark',
  projectName: 'npc-mentality',
  onBrokenLinks: 'warn',
  markdown: {
    hooks: {
      onBrokenMarkdownLinks: 'warn',
    },
  },

  i18n: {
    defaultLocale: 'ko',
    locales: ['ko', 'en', 'ja', 'zh-Hans'],
    localeConfigs: {
      ko: {label: '한국어', direction: 'ltr', htmlLang: 'ko-KR'},
      en: {label: 'English', direction: 'ltr', htmlLang: 'en-US'},
      ja: {label: '日本語', direction: 'ltr', htmlLang: 'ja-JP'},
      'zh-Hans': {label: '中文（简体）', direction: 'ltr', htmlLang: 'zh-CN'},
    },
  },

  presets: [
    [
      'classic',
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
          editUrl:
            'https://github.com/achieveonepark/npc-mentality/tree/main/docs/',
        },
        blog: false,
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      }),
    ],
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      navbar: {
        title: 'NPC Mentality',
        items: [
          {
            type: 'docSidebar',
            sidebarId: 'docsSidebar',
            position: 'left',
            label: '문서',
          },
          {
            href: 'https://github.com/achieveonepark/npc-mentality',
            label: 'GitHub',
            position: 'right',
          },
          {
            type: 'localeDropdown',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: '문서',
            items: [
              {label: '소개', to: '/docs/guide/introduction'},
              {label: '설치', to: '/docs/guide/getting-started'},
            ],
          },
          {
            title: '링크',
            items: [
              {
                label: 'GitHub',
                href: 'https://github.com/achieveonepark/npc-mentality',
              },
            ],
          },
        ],
        copyright: 'Copyright © 2025 AchieveOnePark. MIT License.',
      },
      prism: {
        theme: prismThemes.github,
        darkTheme: prismThemes.dracula,
        additionalLanguages: ['csharp', 'json'],
      },
      colorMode: {
        defaultMode: 'light',
        disableSwitch: false,
        respectPrefersColorScheme: true,
      },
    }),
};

module.exports = config;
