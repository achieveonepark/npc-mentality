---
id: emotion
title: 感情ベースのアニメーションシステム
sidebar_label: 4. 感情アニメーション
sidebar_position: 4
---

# 感情ベースのアニメーションシステム

感情設定の 1 行で移動速度・アニメーター・まばたきが自動で変わります。

## 概要

```
従来:
  anim.Play("Idle");

NPC Mentality:
  emotion.Set(Emotion.Angry);
  → 自動: 歩行速度増加 / 頭が揺れる / 表情変化 / まばたき減少
```

## EmotionType 列挙型

```csharp
public enum EmotionType
{
    Neutral, Happy, Angry, Sad, Afraid, Surprised, Disgusted
}
```

## 感情別エフェクト

| 感情 | NavMesh速度 | アニメータートリガー | BlinkRate |
|------|-------------|---------------------|-----------|
| `Neutral` | ×1.0 | `"Idle"` | `1.0` |
| `Happy` | ×1.0 | `"Happy"` | `1.0` |
| `Angry` | ×1.5 | `"Angry"` | `0.3`（減少） |
| `Sad` | ×0.7 | `"Sad"` | `0.8` |
| `Afraid` | ×1.3 | `"Afraid"` | `1.5`（増加） |

---

## サンプル

```csharp
using NpcMentality;

public class NPCReaction : MonoBehaviour
{
    private NPCMemory _memory;
    private EmotionController _emotion;

    private void Awake()
    {
        _memory  = GetComponent<NPCMemory>();
        _emotion = GetComponent<EmotionController>();
    }

    public void OnTalkWithPlayer()
    {
        float attitude = _memory.GetAttitude();

        if (attitude < -8f)      _emotion.Set(EmotionType.Angry);
        else if (attitude < -2f) _emotion.Set(EmotionType.Disgusted);
        else if (attitude > 8f)  _emotion.Set(EmotionType.Happy);
        else                     _emotion.Set(EmotionType.Neutral);
    }

    public void OnCombatStart() => _emotion.Set(EmotionType.Afraid);
    public void OnCombatEnd()   => _emotion.Set(EmotionType.Neutral);
}
```
