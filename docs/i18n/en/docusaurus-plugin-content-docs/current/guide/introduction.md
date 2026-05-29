---
id: introduction
title: Introduction
sidebar_label: Introduction
sidebar_position: 1
---

# Introduction

**NPC Mentality** is a Unity package with 7 AI systems that make NPCs feel alive.

## Why We Built This

Traditional NPCs follow one of three patterns:

- Move to a target point
- Output pre-written dialogue
- Rush toward the player

NPC Mentality adds layers of **memory, relationships, emotion, and time**. When a player steals from a merchant, the merchant remembers it, rumors spread to friendly neighbors, and the village atmosphere changes — all with a single line of code.

## Systems Overview

| # | System | Key Role |
|---|--------|----------|
| 1 | [NPC Memory](../systems/memory) | Accumulate player actions, calculate attitude |
| 2 | [Relationship Contagion](../systems/relationship) | Spread emotions through friend networks |
| 3 | [Crowd AI](../systems/crowd-ai) | NavMesh-based autonomous crowd behavior |
| 4 | [Emotion Animation](../systems/emotion) | Emotion → speed, animator, blink auto-sync |
| 5 | [Quest Generator](../systems/quest-generator) | Behavior-driven automatic quest generation |
| 6 | [World Time](../systems/world-time) | Game time schedule management |
| 7 | [Rumor System](../systems/rumor) | Information propagation and mutation between NPCs |

## Requirements

- **Unity** 2021.3 LTS or higher
- **NavMesh** — required for Crowd AI and Emotion Animation
- **.NET Standard 2.1**
