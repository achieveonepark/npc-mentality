# 설치

## Package Manager (Git URL)

Unity 에디터에서 **Window → Package Manager → + → Add package from git URL**:

```
https://github.com/achieveonepark/npc-mentality.git
```

## manifest.json 직접 추가

`Packages/manifest.json`을 열고 `dependencies`에 추가:

```json
{
  "dependencies": {
    "com.achieveonepark.npc-mentality": "https://github.com/achieveonepark/npc-mentality.git"
  }
}
```

---

## 빠른 시작

### 1. NPC에 컴포넌트 추가

Inspector에서 NPC GameObject에 필요한 컴포넌트를 추가합니다.

```
Add Component → NPC Mentality → NPC Memory
Add Component → NPC Mentality → Emotion Controller
Add Component → NPC Mentality → Crowd NPC
```

### 2. 플레이어 행동 기록

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

### 3. NPC 반응 읽기

```csharp
float attitude = targetNpc.GetAttitude();

if (attitude < -5f)
    dialogue.Say("또 왔네... 내 물건은 건들지 마.");
else if (attitude > 5f)
    dialogue.Say("지난번엔 고마웠어.");
else
    dialogue.Say("어서오세요.");
```

### 4. 시간 시스템 등록

씬에 빈 GameObject를 만들고 `WorldTimeSystem` 컴포넌트를 추가합니다.

```csharp
void Start()
{
    WorldTimeSystem.Instance.Register("Shop",
        openHour: 8,
        closeHour: 22,
        onOpen:  () => Debug.Log("상점 열림"),
        onClose: () => Debug.Log("상점 닫힘")
    );
}
```

---

## 에디터 창

**Tools → NPC Mentality** 메뉴에서 7개 시스템의 API 레퍼런스를 확인할 수 있습니다.

## 샘플

Package Manager에서 **Samples → Basic Demo**를 임포트하면 `DemoUsage.cs`가 추가됩니다.  
7개 시스템을 한 씬에서 연결한 예시 코드입니다.
