---
id: getting-started
title: Installation & Quick Start
sidebar_label: Installation & Quick Start
sidebar_position: 2
---

# Installation

## Package Manager (Git URL)

In the Unity Editor, go to **Window → Package Manager → + → Add package from git URL**:

```
https://github.com/achieveonepark/npc-mentality.git
```

## Direct manifest.json Edit

Open `Packages/manifest.json` and add to `dependencies`:

```json
{
  "dependencies": {
    "com.achieveonepark.npc-mentality": "https://github.com/achieveonepark/npc-mentality.git"
  }
}
```

---

## Quick Start

### 1. Add Components to NPC

Add the required components to the NPC GameObject in the Inspector.

```
Add Component → NPC Mentality → NPC Memory
Add Component → NPC Mentality → Emotion Controller
Add Component → NPC Mentality → Crowd NPC
```

### 2. Record Player Actions

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

### 3. Read NPC Reaction

```csharp
float attitude = targetNpc.GetAttitude();

if (attitude < -5f)
    dialogue.Say("You again... keep your hands off my stuff.");
else if (attitude > 5f)
    dialogue.Say("Thanks for your help last time.");
else
    dialogue.Say("Welcome.");
```

### 4. Register Time Schedule

Create an empty GameObject in the scene and add the `WorldTimeSystem` component.

```csharp
void Start()
{
    WorldTimeSystem.Instance.Register("Shop",
        openHour: 8,
        closeHour: 22,
        onOpen:  () => Debug.Log("Shop opened"),
        onClose: () => Debug.Log("Shop closed")
    );
}
```

---

## Editor Window

Use **Tools → NPC Mentality** to view the API reference for all 7 systems.

## Sample

Import **Samples → Basic Demo** from the Package Manager to get `DemoUsage.cs` — a single scene demonstrating all 7 systems connected together.
