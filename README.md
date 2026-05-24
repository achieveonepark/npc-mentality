# NPC Mentality

Unity 패키지 — NPC를 살아있게 만드는 7가지 AI 시스템.

```
com.achieveonepark.npc-mentality | Unity 2021.3+
```

---

## 시스템 목록

### 1. NPC 기억 시스템
NPC가 플레이어 행동을 기억하고 누적 태도(Attitude)를 계산합니다.

```csharp
npc.Remember(EventType.Steal, weight: -3f);
npc.Remember(EventType.Help,  weight:  5f);

float attitude = npc.GetAttitude(); // 양수 = 우호적, 음수 = 적대적
bool stolenBefore = npc.HasMemory(EventType.Steal);
```

### 2. 관계 전염 시스템
A가 플레이어를 싫어하면, A와 친한 B도 영향받고, B와 친한 C도 약간 영향받습니다.

```csharp
// RelationshipGraph (ScriptableObject) 에 친구 관계 등록
graph.AddFriendship("merchant_a", "villager_b");

// 감정 전파 (depth=2, decay=0.5)
RelationshipContagion.Propagate("merchant_a", -10f, graph, allNpcs, depth: 2);
```

### 3. 자연스러운 군중 AI
NavMeshAgent 기반으로 배회·가게 구경·주변 관찰·갑자기 멈춤·길 우회를 자동 수행합니다.

```csharp
// CrowdNPC 컴포넌트를 NPC GameObject에 추가하면 자동 동작
// Inspector에서 WanderRadius, BehaviorChangeCooldown 조정 가능
```

### 4. 감정 기반 애니메이션 시스템
감정 설정 한 줄로 이동속도·애니메이터 트리거·눈 깜빡임 속도가 자동 변경됩니다.

```csharp
emotion.Set(EmotionType.Angry);
// → 이동속도 1.5x, Animator "Angry" 트리거, BlinkRate 0.3
```

| 감정 | 속도 | 눈 깜빡임 |
|------|------|----------|
| Angry | ×1.5 | 감소 |
| Happy | ×1.0 | 보통 |
| Sad | ×0.7 | 약간 감소 |
| Afraid | ×1.3 | 증가 |
| Neutral | 기본 | 보통 |

### 5. AI 퀘스트 생성기
플레이어 행동 기반으로 맥락에 맞는 퀘스트를 자동 생성합니다.

```csharp
// 플레이어가 숲에서 늑대를 많이 잡음
QuestData q = generator.GenerateQuest(EventType.Kill, "wolf");
// → "숲 근처에 이상한 wolf가 나타났다."

generator.OnQuestGenerated += quest => Debug.Log(quest.Description);
```

### 6. 시간 흐름 세계 시스템
게임 내 시간 흐름에 따라 NPC·상점의 스케줄을 관리합니다.

```csharp
WorldTimeSystem.Instance.Register("Shop",
    openHour: 8, closeHour: 22,
    onOpen:  () => shopNpc.Open(),
    onClose: () => shopNpc.Close()
);

// 08:00 상점 오픈 / 12:00 NPC 식사 / 22:00 상점 닫힘 / 02:00 도둑 등장
bool isOpen = WorldTimeSystem.Instance.IsOpen("Shop");
```

### 7. 루머 시스템
루머가 NPC 사이를 돌면서 내용이 변형됩니다.

```csharp
rumorSystem.SpreadRumor("누군가 용을 잡았다", sourceNpcId: "player");
rumorSystem.PassRumor(rumor, "npc_01"); // "누가 용을 잡았다던데?"
rumorSystem.PassRumor(rumor, "npc_02"); // "아니던데? 마법사래"
rumorSystem.PassRumor(rumor, "npc_03"); // "마왕 죽었대"
```

---

## 설치

Unity Package Manager → Add package from git URL:
```
https://github.com/achieveonepark/npc-mentality.git
```

또는 `Packages/manifest.json`에 직접 추가:
```json
"com.achieveonepark.npc-mentality": "https://github.com/achieveonepark/npc-mentality.git"
```

## 요구사항

- Unity 2021.3 이상
- NavMesh (군중 AI, 감정 애니메이션 사용 시)

## 에디터 도구

`Tools > NPC Mentality` — 7개 시스템 API 레퍼런스 창
