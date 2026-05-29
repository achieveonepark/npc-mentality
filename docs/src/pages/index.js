import React from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import Layout from '@theme/Layout';
import Translate, {translate} from '@docusaurus/Translate';
import styles from './index.module.css';

const features = [
  {
    emoji: '🧠',
    titleId: 'feature.memory.title',
    defaultTitle: 'NPC Memory',
    descId: 'feature.memory.desc',
    defaultDesc: 'NPCs remember player actions and calculate attitude over time.',
  },
  {
    emoji: '🕸️',
    titleId: 'feature.relationship.title',
    defaultTitle: 'Relationship Contagion',
    descId: 'feature.relationship.desc',
    defaultDesc: "If A dislikes you, A's friends and their friends are affected too.",
  },
  {
    emoji: '🚶',
    titleId: 'feature.crowd.title',
    defaultTitle: 'Crowd AI',
    descId: 'feature.crowd.desc',
    defaultDesc: 'Wander, browse shops, look around, stop, and detour — natural crowd behavior.',
  },
  {
    emoji: '😠',
    titleId: 'feature.emotion.title',
    defaultTitle: 'Emotion Animation',
    descId: 'feature.emotion.desc',
    defaultDesc: 'One line sets speed, animator triggers, and blink rate automatically.',
  },
  {
    emoji: '📜',
    titleId: 'feature.quest.title',
    defaultTitle: 'AI Quest Generator',
    descId: 'feature.quest.desc',
    defaultDesc: 'Player behavior patterns automatically generate contextual quests.',
  },
  {
    emoji: '🕐',
    titleId: 'feature.time.title',
    defaultTitle: 'World Time',
    descId: 'feature.time.desc',
    defaultDesc: 'Shop open at 08:00, lunch at 12:00, thief at 02:00 — register in one line.',
  },
  {
    emoji: '💬',
    titleId: 'feature.rumor.title',
    defaultTitle: 'Rumor System',
    descId: 'feature.rumor.desc',
    defaultDesc: 'Rumors mutate as they pass through NPCs, just like real gossip.',
  },
];

function Feature({emoji, titleId, defaultTitle, descId, defaultDesc}) {
  return (
    <div className={clsx('col col--3', styles.featureCard)}>
      <div className={styles.featureEmoji}>{emoji}</div>
      <h3>
        <Translate id={titleId}>{defaultTitle}</Translate>
      </h3>
      <p>
        <Translate id={descId}>{defaultDesc}</Translate>
      </p>
    </div>
  );
}

export default function Home() {
  const {siteConfig} = useDocusaurusContext();
  return (
    <Layout
      title={siteConfig.title}
      description={translate({
        id: 'homepage.description',
        message: 'Unity NPC AI Package — Memory, Relationship, Crowd, Emotion, Quest, Time, Rumor',
      })}>
      <header className={clsx('hero hero--primary', styles.heroBanner)}>
        <div className="container">
          <h1 className="hero__title">{siteConfig.title}</h1>
          <p className="hero__subtitle">
            <Translate id="homepage.tagline">
              Bring NPCs to Life
            </Translate>
          </p>
          <p className={styles.heroSubtext}>
            <Translate id="homepage.subtext">
              Unity NPC AI Package — Memory · Relationship · Crowd · Emotion · Quest · Time · Rumor
            </Translate>
          </p>
          <div className={styles.buttons}>
            <Link
              className="button button--secondary button--lg"
              to="/docs/guide/getting-started">
              <Translate id="homepage.getStarted">Get Started</Translate>
            </Link>
            <Link
              className={clsx('button button--outline button--secondary button--lg', styles.githubBtn)}
              href="https://github.com/achieveonepark/npc-mentality">
              GitHub
            </Link>
          </div>
        </div>
      </header>
      <main>
        <section className={styles.features}>
          <div className="container">
            <div className="row">
              {features.map((props, idx) => (
                <Feature key={idx} {...props} />
              ))}
            </div>
          </div>
        </section>
      </main>
    </Layout>
  );
}
