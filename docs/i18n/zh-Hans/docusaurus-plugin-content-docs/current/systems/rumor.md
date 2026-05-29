---
id: rumor
title: 谣言系统
sidebar_label: 7. 谣言系统
sidebar_position: 7
---

# 谣言系统

信息在 NPC 之间流传时内容逐渐变形。

## 概述

```
玩家击败了Boss
    ↓
NPC1: "听说有人杀了一条龙？"
    ↓
NPC2: "不对吧？据说是个魔法师。"
    ↓
NPC3: "听说魔王死了。"
```

## 组件

`RumorSystem` — MonoBehaviour。

| 字段 | 类型 | 默认值 | 说明 |
|------|------|-------|------|
| `MutationChance` | `float` | `0.3f` | 谣言变形概率（0~1） |
| `MutationSuffixes` | `List<string>` | 默认4个 | 变形时附加的句子 |
| `ActiveRumors` | `List<RumorData>` | — | 当前进行中的谣言列表 |

---

## API

```csharp
rumorSystem.SpreadRumor("有人杀了一条龙", sourceNpcId: "player");

var rumor = rumorSystem.ActiveRumors[^1];
rumorSystem.PassRumor(rumor, "npc_01");
```

---

## 示例

```csharp
using NpcMentality;

public class RumorDemo : MonoBehaviour
{
    public RumorSystem rumorSystem;
    public string[] npcIds = { "blacksmith", "innkeeper", "farmer", "guard" };

    public void OnPlayerKillsDragon()
    {
        rumorSystem.SpreadRumor("有人杀了一条龙", "player");
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

## 提示

- `MutationChance = 0f` — 不变形，只传播事实。
- `MutationChance = 1f` — 每次传递都必定变形。
- 与 [RelationshipContagion](./relationship) 结合，同时传播谣言内容和情感。
- 利用 `HopCount` 实现"这个谣言不可信"的 UI。
