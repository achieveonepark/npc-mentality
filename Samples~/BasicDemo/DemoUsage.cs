using System.Collections.Generic;
using UnityEngine;

namespace NpcMentality.Samples
{
    /// <summary>
    /// NPC Mentality 7개 시스템 사용 예시입니다.
    /// 씬에 이 컴포넌트를 붙이고 필드를 인스펙터에서 연결하세요.
    /// </summary>
    public class DemoUsage : MonoBehaviour
    {
        [Header("Memory")]
        public NPCMemory MerchantMemory;
        public NPCMemory VillagerMemory;

        [Header("Relationship")]
        public RelationshipGraph RelationshipGraph;

        [Header("Emotion")]
        public EmotionController MerchantEmotion;

        [Header("Quest")]
        public AIQuestGenerator QuestGenerator;

        [Header("World Time")]
        public WorldTimeSystem WorldTime;

        [Header("Rumor")]
        public RumorSystem RumorSystem;

        private Dictionary<string, NPCMemory> _allNpcs;

        private void Start()
        {
            // ── 1. NPC Memory ──────────────────────────────────────────────
            // 플레이어가 상인 물건을 훔침
            MerchantMemory.Remember(EventType.Steal, weight: -3f);
            MerchantMemory.Remember(EventType.Steal, weight: -3f);
            MerchantMemory.Remember(EventType.Steal, weight: -3f);

            // 마을 주민을 구해줬음
            VillagerMemory.Remember(EventType.Rescue, weight: 5f);

            Debug.Log($"[Memory] 상인 태도: {MerchantMemory.GetAttitude()}");   // -9
            Debug.Log($"[Memory] 주민 태도: {VillagerMemory.GetAttitude()}");   // +5

            // ── 2. Relationship Contagion ──────────────────────────────────
            _allNpcs = new Dictionary<string, NPCMemory>
            {
                { "merchant", MerchantMemory },
                { "villager", VillagerMemory }
            };

            // 상인 싫어함이 친한 주민에게도 약하게 전파
            RelationshipContagion.Propagate("merchant", -10f, RelationshipGraph, _allNpcs, depth: 2);

            // ── 3. Crowd AI ────────────────────────────────────────────────
            // CrowdNPC 컴포넌트가 씬에 부착되어 있으면 자동으로 동작합니다.
            // 별도 코드 불필요.

            // ── 4. Emotion Animation ───────────────────────────────────────
            if (MerchantMemory.GetAttitude() < -5f)
                MerchantEmotion.Set(EmotionType.Angry);
            else
                MerchantEmotion.Set(EmotionType.Neutral);

            // ── 5. AI Quest Generator ──────────────────────────────────────
            QuestGenerator.OnQuestGenerated += quest =>
                Debug.Log($"[Quest] 생성됨: {quest.Title} — {quest.Description}");

            // 플레이어가 숲에서 늑대를 많이 잡음 → 퀘스트 생성
            QuestData q = QuestGenerator.GenerateQuest(EventType.Kill, "wolf");
            Debug.Log($"[Quest] {q.Title}");

            // ── 6. World Time ──────────────────────────────────────────────
            WorldTime.Register("Shop", openHour: 8, closeHour: 22,
                onOpen:  () => Debug.Log("[WorldTime] 상점 오픈"),
                onClose: () => Debug.Log("[WorldTime] 상점 닫힘"));

            WorldTime.Register("Thief", openHour: 2, closeHour: 4,
                onOpen:  () => Debug.Log("[WorldTime] 도둑 등장!"),
                onClose: () => Debug.Log("[WorldTime] 도둑 사라짐"));

            WorldTime.OnHourChanged += h => Debug.Log($"[WorldTime] 현재 시간: {h:F1}시");

            // ── 7. Rumor System ────────────────────────────────────────────
            RumorSystem.OnRumorSpread += r =>
                Debug.Log($"[Rumor] 루머 발생: \"{r.CurrentVersion}\"");

            RumorSystem.OnRumorPassed += (r, npcId) =>
                Debug.Log($"[Rumor] {npcId}에게 전달됨 (hop {r.HopCount}): \"{r.CurrentVersion}\"");

            // 플레이어가 보스(용)를 잡음
            RumorSystem.SpreadRumor("누군가 용을 잡았다", "player");
            var rumor = RumorSystem.ActiveRumors[0];

            // NPC 사이를 거치며 내용 변형
            RumorSystem.PassRumor(rumor, "npc_01");  // "누가 용을 잡았다던데?"
            RumorSystem.PassRumor(rumor, "npc_02");  // "아니던데? 마법사래"
            RumorSystem.PassRumor(rumor, "npc_03");  // "마왕 죽었대"
        }
    }
}
