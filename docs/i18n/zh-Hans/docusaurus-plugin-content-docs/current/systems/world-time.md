---
id: world-time
title: 时间流逝世界系统
sidebar_label: 6. 时间流逝世界
sidebar_position: 6
---

# 时间流逝世界系统

根据游戏内时间流逝自动管理商店开关和 NPC 时间表。

## 概述

```
08:00 → 商店开门
12:00 → NPC 用餐
22:00 → 商店关门
02:00 → 小偷出现
```

## 组件

`WorldTimeSystem` — MonoBehaviour 单例。每个场景只放一个。

| 字段 | 类型 | 默认值 | 说明 |
|------|------|-------|------|
| `TimeScale` | `float` | `1f` | 时间倍速 |
| `CurrentHour` | `float` | `0f` | 当前时间（0~24，只读） |

---

## API

```csharp
WorldTimeSystem.Instance.Register(
    "Shop",
    openHour: 8,
    closeHour: 22,
    onOpen:  () => shopkeeper.Open(),
    onClose: () => shopkeeper.Close()
);

if (WorldTimeSystem.Instance.IsOpen("Shop"))
    ShowShopUI();

WorldTimeSystem.Instance.OnHourChanged += hour =>
    Debug.Log($"当前时间: {Mathf.FloorToInt(hour):D2}:00");
```

---

## 示例

```csharp
using NpcMentality;

public class VillageScheduler : MonoBehaviour
{
    public CrowdNPC[] villagers;
    public GameObject thiefPrefab;

    private void Start()
    {
        var wt = WorldTimeSystem.Instance;
        wt.Register("Day", openHour: 6, closeHour: 22,
            onOpen:  () => { foreach (var v in villagers) v.enabled = true; },
            onClose: () => { foreach (var v in villagers) v.enabled = false; });

        wt.Register("Thief", openHour: 2, closeHour: 4,
            onOpen:  () => Instantiate(thiefPrefab, GetAlleyPosition(), Quaternion.identity),
            onClose: () => DespawnThieves());
    }
}
```
