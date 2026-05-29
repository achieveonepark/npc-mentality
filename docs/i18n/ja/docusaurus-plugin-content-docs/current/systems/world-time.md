---
id: world-time
title: 時間流れの世界システム
sidebar_label: 6. 時間流れの世界
sidebar_position: 6
---

# 時間流れの世界システム

ゲーム内の時間の流れに応じて、店の開閉や NPC のスケジュールを自動管理します。

## 概要

```
08:00 → 店が開く
12:00 → NPC が昼食
22:00 → 店が閉まる
02:00 → 盗賊が登場
```

## コンポーネント

`WorldTimeSystem` — MonoBehaviour シングルトン。シーンに 1 つだけ配置します。

| フィールド | 型 | デフォルト | 説明 |
|------------|-----|-----------|------|
| `TimeScale` | `float` | `1f` | 時間倍速 |
| `CurrentHour` | `float` | `0f` | 現在時刻（0〜24、読み取り専用） |

---

## API

### Register

```csharp
WorldTimeSystem.Instance.Register(
    "Shop",
    openHour: 8,
    closeHour: 22,
    onOpen:  () => shopkeeper.Open(),
    onClose: () => shopkeeper.Close()
);
```

### IsOpen

```csharp
if (WorldTimeSystem.Instance.IsOpen("Shop"))
    ShowShopUI();
```

### OnHourChanged イベント

```csharp
WorldTimeSystem.Instance.OnHourChanged += hour =>
    Debug.Log($"現在時刻: {Mathf.FloorToInt(hour):D2}:00");
```

---

## サンプル

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
