---
id: introduction
title: 소개
sidebar_label: 소개
sidebar_position: 1
---

# 소개

**NPC Mentality**는 Unity 게임의 NPC를 살아있는 존재처럼 만드는 7개 AI 시스템 패키지입니다.

## 왜 만들었나

기존 NPC는 세 가지 패턴 중 하나입니다.

- 목표 지점으로 이동
- 미리 짜인 대사 출력
- 플레이어를 향해 돌진

NPC Mentality는 여기에 **기억·관계·감정·시간** 레이어를 얹습니다. 플레이어가 상인의 물건을 훔치면 그 상인이 기억하고, 친한 이웃에게 소문이 퍼지고, 마을 분위기가 달라집니다. 코드 한 줄로요.

## 시스템 구성

| # | 시스템 | 핵심 역할 |
|---|--------|----------|
| 1 | [NPC 기억](../systems/memory) | 플레이어 행동 누적, 태도 계산 |
| 2 | [관계 전염](../systems/relationship) | 친구 네트워크 통한 감정 전파 |
| 3 | [군중 AI](../systems/crowd-ai) | NavMesh 기반 자율 군중 행동 |
| 4 | [감정 애니메이션](../systems/emotion) | 감정 → 속도·애니메이터 자동 연동 |
| 5 | [퀘스트 생성](../systems/quest-generator) | 행동 기반 퀘스트 자동 생성 |
| 6 | [시간 세계](../systems/world-time) | 게임 시간 스케줄 관리 |
| 7 | [루머](../systems/rumor) | NPC 간 정보 전파 및 변형 |

## 요구사항

- **Unity** 2021.3 LTS 이상
- **NavMesh** — 군중 AI, 감정 애니메이션 사용 시
- **.NET Standard 2.1**
