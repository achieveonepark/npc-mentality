---
id: getting-started
title: インストールとクイックスタート
sidebar_label: インストールとクイックスタート
sidebar_position: 2
---

# インストール

## Package Manager（Git URL）

Unity エディターで **Window → Package Manager → + → Add package from git URL**:

```
https://github.com/achieveonepark/npc-mentality.git
```

## manifest.json に直接追加

`Packages/manifest.json` を開き、`dependencies` に追加します:

```json
{
  "dependencies": {
    "com.achieveonepark.npc-mentality": "https://github.com/achieveonepark/npc-mentality.git"
  }
}
```

---

## クイックスタート

### 1. NPC にコンポーネントを追加

Inspector で NPC の GameObject に必要なコンポーネントを追加します。

```
Add Component → NPC Mentality → NPC Memory
Add Component → NPC Mentality → Emotion Controller
Add Component → NPC Mentality → Crowd NPC
```

### 2. プレイヤー行動を記録

```csharp
using NpcMentality;

public class PlayerInteraction : MonoBehaviour
{
    public NPCMemory targetNpc;

    void StealItem()
    {
        targetNpc.Remember(EventType.Steal, weight: -3f);
    }

    void HelpVillager()
    {
        targetNpc.Remember(EventType.Help, weight: 5f);
    }
}
```

### 3. NPC の反応を読む

```csharp
float attitude = targetNpc.GetAttitude();

if (attitude < -5f)
    dialogue.Say("また来たのか…荷物に触るなよ。");
else if (attitude > 5f)
    dialogue.Say("前回は助かったよ。");
else
    dialogue.Say("いらっしゃいませ。");
```

### 4. 時間システムを登録

シーンに空の GameObject を作り、`WorldTimeSystem` コンポーネントを追加します。

```csharp
void Start()
{
    WorldTimeSystem.Instance.Register("Shop",
        openHour: 8,
        closeHour: 22,
        onOpen:  () => Debug.Log("店が開いた"),
        onClose: () => Debug.Log("店が閉まった")
    );
}
```

---

## エディターウィンドウ

**Tools → NPC Mentality** メニューで 7 つのシステムの API リファレンスを確認できます。

## サンプル

Package Manager で **Samples → Basic Demo** をインポートすると `DemoUsage.cs` が追加されます。7 つのシステムを 1 つのシーンで接続したサンプルコードです。
