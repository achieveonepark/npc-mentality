---
id: getting-started
title: 安装与快速入门
sidebar_label: 安装与快速入门
sidebar_position: 2
---

# 安装

## Package Manager（Git URL）

在 Unity 编辑器中选择 **Window → Package Manager → + → Add package from git URL**：

```
https://github.com/achieveonepark/npc-mentality.git
```

## 直接编辑 manifest.json

打开 `Packages/manifest.json`，在 `dependencies` 中添加：

```json
{
  "dependencies": {
    "com.achieveonepark.npc-mentality": "https://github.com/achieveonepark/npc-mentality.git"
  }
}
```

---

## 快速入门

### 1. 向 NPC 添加组件

在 Inspector 中向 NPC GameObject 添加所需组件。

```
Add Component → NPC Mentality → NPC Memory
Add Component → NPC Mentality → Emotion Controller
Add Component → NPC Mentality → Crowd NPC
```

### 2. 记录玩家行为

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

### 3. 读取 NPC 反应

```csharp
float attitude = targetNpc.GetAttitude();

if (attitude < -5f)
    dialogue.Say("你又来了...别动我的东西。");
else if (attitude > 5f)
    dialogue.Say("上次谢谢你的帮助。");
else
    dialogue.Say("欢迎光临。");
```

### 4. 注册时间系统

在场景中创建一个空 GameObject，并添加 `WorldTimeSystem` 组件。

```csharp
void Start()
{
    WorldTimeSystem.Instance.Register("Shop",
        openHour: 8,
        closeHour: 22,
        onOpen:  () => Debug.Log("商店开门了"),
        onClose: () => Debug.Log("商店关门了")
    );
}
```

---

## 编辑器窗口

使用 **Tools → NPC Mentality** 查看所有 7 个系统的 API 参考。

## 示例

从 Package Manager 中导入 **Samples → Basic Demo** 可以获取 `DemoUsage.cs`，这是一个在单个场景中连接所有 7 个系统的示例代码。
