---
id: quest-generator
title: AIクエスト生成器
sidebar_label: 5. AIクエスト生成器
sidebar_position: 5
---

# AIクエスト生成器

プレイヤーの行動パターンを分析して、文脈に合ったクエストを自動生成します。

## 概要

```
プレイヤー:
  - 森によく行く
  - オオカミをたくさん倒す

生成:
  "森の近くに奇妙なオオカミが現れた。"

ランダムクエストよりもずっと自然。
```

## API

### GenerateQuest

```csharp
QuestData GenerateQuest(EventType trigger, string subject = "creature")
```

```csharp
QuestData q = generator.GenerateQuest(EventType.Kill, "wolf");
// → "森の近くに奇妙なwolfが現れた。"
```

### OnQuestGenerated イベント

```csharp
generator.OnQuestGenerated += quest =>
{
    QuestUI.Show(quest.Title, quest.Description);
    QuestLog.Add(quest);
};
```

---

## デフォルトテンプレート

| EventType | 生成例 |
|-----------|-------|
| `Kill` | "森の近くに奇妙な {subject} が現れた。" |
| `Steal` | "商人地区から荷物が消えた。" |
| `Help` | "また助けの手が必要だ。" |
| `Rescue` | "危険にさらされた人がいる。" |

---

## サンプル

```csharp
using NpcMentality;

public class PlayerBehaviorTracker : MonoBehaviour
{
    public AIQuestGenerator questGenerator;
    private int _wolfKillCount = 0;

    public void OnWolfKilled()
    {
        _wolfKillCount++;
        if (_wolfKillCount >= 5)
        {
            questGenerator.GenerateQuest(EventType.Kill, "wolf");
            _wolfKillCount = 0;
        }
    }
}
```
