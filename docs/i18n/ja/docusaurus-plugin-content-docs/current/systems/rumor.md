---
id: rumor
title: 噂システム
sidebar_label: 7. 噂システム
sidebar_position: 7
---

# 噂システム

情報が NPC の間を流れるにつれて内容が変化していきます。

## 概要

```
プレイヤーがボスを倒す
    ↓
NPC1: "誰かがドラゴンを倒したって？"
    ↓
NPC2: "違うよ、魔法使いらしいよ"
    ↓
NPC3: "魔王が死んだらしい"
```

## コンポーネント

`RumorSystem` — MonoBehaviour。

| フィールド | 型 | デフォルト | 説明 |
|------------|-----|-----------|------|
| `MutationChance` | `float` | `0.3f` | 変形確率（0〜1） |
| `MutationSuffixes` | `List<string>` | デフォルト4つ | 変形時に追加される文章 |
| `ActiveRumors` | `List<RumorData>` | — | 現在進行中の噂リスト |

---

## API

```csharp
rumorSystem.SpreadRumor("誰かがドラゴンを倒した", sourceNpcId: "player");

var rumor = rumorSystem.ActiveRumors[^1];
rumorSystem.PassRumor(rumor, "npc_01");
```

---

## サンプル

```csharp
using NpcMentality;

public class RumorDemo : MonoBehaviour
{
    public RumorSystem rumorSystem;
    public string[] npcIds = { "blacksmith", "innkeeper", "farmer", "guard" };

    public void OnPlayerKillsDragon()
    {
        rumorSystem.SpreadRumor("誰かがドラゴンを倒した", "player");
        StartCoroutine(SpreadToVillage());
    }

    private System.Collections.IEnumerator SpreadToVillage()
    {
        var rumor = rumorSystem.ActiveRumors[^1];
        foreach (string npcId in npcIds)
        {
            yield return new WaitForSeconds(2f);
            rumorSystem.PassRumor(rumor, npcId);
        }
    }
}
```

---

## ヒント

- `MutationChance = 0f` — 変形なしで事実のみ伝播します。
- `MutationChance = 1f` — 毎回必ず変形します。
- [RelationshipContagion](./relationship) と組み合わせると噂の内容と感情が同時に広がります。
