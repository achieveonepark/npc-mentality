import { defineConfig } from 'vitepress'

export default defineConfig({
  title: 'NPC Mentality',
  description: 'Unity NPC AI 시스템 — 기억, 관계, 군중, 감정, 퀘스트, 시간, 루머',
  lang: 'ko-KR',

  themeConfig: {
    logo: '/logo.png',
    siteTitle: 'NPC Mentality',

    nav: [
      { text: '시작하기', link: '/guide/getting-started' },
      { text: '시스템', link: '/systems/memory' },
      { text: 'GitHub', link: 'https://github.com/achieveonepark/npc-mentality' },
    ],

    sidebar: [
      {
        text: '가이드',
        items: [
          { text: '소개', link: '/guide/introduction' },
          { text: '설치', link: '/guide/getting-started' },
        ],
      },
      {
        text: '시스템',
        items: [
          { text: '1. NPC 기억 시스템', link: '/systems/memory' },
          { text: '2. 관계 전염 시스템', link: '/systems/relationship' },
          { text: '3. 자연스러운 군중 AI', link: '/systems/crowd-ai' },
          { text: '4. 감정 기반 애니메이션', link: '/systems/emotion' },
          { text: '5. AI 퀘스트 생성기', link: '/systems/quest-generator' },
          { text: '6. 시간 흐름 세계', link: '/systems/world-time' },
          { text: '7. 루머 시스템', link: '/systems/rumor' },
        ],
      },
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/achieveonepark/npc-mentality' },
    ],

    footer: {
      message: 'MIT License',
      copyright: 'Copyright © 2025 AchieveOnePark',
    },

    search: {
      provider: 'local',
    },
  },
})
