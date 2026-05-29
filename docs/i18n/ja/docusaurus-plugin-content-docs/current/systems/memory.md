---
id: memory
title: NPC記憶システム
sidebar_label: 1. NPC記憶システム
sidebar_position: 1
---

# NPC記憶システム

NPC がプレイヤーの行動を記憶し、累積態度（Attitude）を計算します。

## 概要

```
プレイヤー:
- 商人の物を3回盗んだ
- 村人を助けた

NPC:
  商人: "また来たのか…荷物に触るなよ。"
  村人: "前回は助かったよ。"
```

## コンポーネント

`NPCMemory` — MonoBehaviour。NPC の GameObject に追加します。

| フィールド | 型 | デフォルト | 説明 |
|------------|-----|-----------|------|
| `MaxMemories` | `int` | `20` | 最大記憶数。超過時は最も古いものを削除 |
| `memories` | `List<MemoryEvent>` | — | 記憶リスト（Inspector で確認可能） |

## API

### Remember

```csharp
void Remember(EventType type, float weight = 1f, string context = "")
```

プレイヤー行動を記録します。

| パラメーター | 説明 |
|-------------|------|
| `type` | 行動タイプ（`EventType` 列挙型） |
| `weight` | 態度への影響。正 = 友好的、負 = 敵対的 |
| `context` | 任意の説明文字列 |

```csharp
npc.Remember(EventType.Steal,  weight: -3f);
npc.Remember(EventType.Help,   weight:  5f);
npc.Remember(EventType.Attack, weight: -10f);
npc.Remember(EventType.Gift,   weight:  3f);
```

### GetAttitude

```csharp
float GetAttitude()
```

全記憶の weight 合計を返します。

```csharp
float attitude = npc.GetAttitude();
// attitude > 0  → 友好的
// attitude == 0 → 中立
// attitude < 0  → 敵対的
```

### HasMemory

```csharp
bool HasMemory(EventType type)
```

特定タイプの記憶が存在するか確認します。

```csharp
if (npc.HasMemory(EventType.Steal))
    dialogue.Say("また来たのか…");
```

### ForgetOldest

```csharp
void ForgetOldest()
```

最も古い記憶を手動で削除します。`MaxMemories` 超過時に自動呼び出しされます。

---

## EventType 列挙型

```csharp
public enum EventType
{
    None, Steal, Help, Attack, Trade,
    Talk, Threaten, Gift, Kill, Rescue
}
```

---

## サンプル

```csharp
using NpcMentality;

public class ShopKeeper : MonoBehaviour
{
    private NPCMemory _memory;

    private void Awake() => _memory = GetComponent<NPCMemory>();

    public void OnPlayerSteals()
    {
        _memory.Remember(EventType.Steal, weight: -3f, context: "貴重な薬草");

        if (_memory.GetAttitude() < -6f)
            GuardSystem.Alert(transform.position);
    }

    public string GetGreeting()
    {
        return _memory.GetAttitude() switch
        {
            < -5f => "出て行け。",
            < 0f  => "何が入り用だ？",
            < 5f  => "いらっしゃいませ。",
            _     => "あなたのようなお客様が増えるといいですね！"
        };
    }
}
```
