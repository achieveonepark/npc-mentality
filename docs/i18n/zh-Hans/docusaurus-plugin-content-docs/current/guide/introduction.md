---
id: introduction
title: 简介
sidebar_label: 简介
sidebar_position: 1
---

# 简介

**NPC Mentality** 是一个 Unity 包，包含 7 个 AI 系统，让 NPC 感觉真实存在。

## 为什么要做这个

传统 NPC 遵循以下三种模式之一：

- 移动到目标点
- 输出预设对话
- 向玩家冲刺

NPC Mentality 在此基础上增加了**记忆、关系、情感、时间**层。当玩家从商人处偷窃时，商人会记住它，谣言会传播给亲近的邻居，整个村子的氛围都会改变——只需一行代码。

## 系统构成

| # | 系统 | 核心作用 |
|---|------|---------|
| 1 | [NPC记忆](../systems/memory) | 累积玩家行为，计算态度 |
| 2 | [关系传染](../systems/relationship) | 通过朋友网络传播情感 |
| 3 | [群体AI](../systems/crowd-ai) | 基于NavMesh的自主群体行为 |
| 4 | [情感动画](../systems/emotion) | 情感 → 速度、动画器自动联动 |
| 5 | [任务生成](../systems/quest-generator) | 基于行为的自动任务生成 |
| 6 | [时间世界](../systems/world-time) | 游戏时间调度管理 |
| 7 | [谣言系统](../systems/rumor) | NPC间信息传播与变形 |

## 需求

- **Unity** 2021.3 LTS 或更高版本
- **NavMesh** — 使用群体AI和情感动画时需要
- **.NET Standard 2.1**
