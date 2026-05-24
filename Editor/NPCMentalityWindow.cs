using UnityEditor;
using UnityEngine;

namespace NpcMentality.Editor
{
    public class NPCMentalityWindow : EditorWindow
    {
        private Vector2 _scroll;

        private static readonly (string name, string desc, string api)[] Systems =
        {
            ("1. NPC Memory", "NPC가 플레이어 행동을 기억하고 태도를 누적합니다.",
             "npc.Remember(EventType.Steal);\nfloat attitude = npc.GetAttitude();"),

            ("2. Relationship Contagion", "NPC 간 감정이 친구 관계를 통해 전파됩니다.",
             "RelationshipContagion.Propagate(sourceId, -10f, graph, allNpcs, depth:2);"),

            ("3. Crowd AI", "NavMeshAgent 기반 자연스러운 군중 행동입니다.",
             "// CrowdNPC 컴포넌트 추가 — 자동으로 배회·관찰·멈춤 등을 수행합니다."),

            ("4. Emotion Animation", "감정 설정 시 이동속도·애니메이터·눈 깜빡임이 자동 변경됩니다.",
             "emotion.Set(EmotionType.Angry);"),

            ("5. AI Quest Generator", "플레이어 행동 패턴 기반으로 퀘스트를 자동 생성합니다.",
             "QuestData q = generator.GenerateQuest(EventType.Kill, \"wolf\");"),

            ("6. World Time", "게임 시간에 따라 상점 오픈/닫힘 콜백을 등록합니다.",
             "WorldTimeSystem.Instance.Register(\"Shop\", openHour:8, closeHour:22);"),

            ("7. Rumor System", "루머가 NPC 사이를 돌면서 변형됩니다.",
             "rumorSystem.SpreadRumor(\"용을 잡았다\", \"player\");\nrumorSystem.PassRumor(rumor, \"npc_02\");"),
        };

        [MenuItem("Tools/NPC Mentality")]
        public static void Open() => GetWindow<NPCMentalityWindow>("NPC Mentality");

        private void OnGUI()
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("NPC Mentality  v1.0.0", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("com.achieveonepark.npc-mentality", EditorStyles.miniLabel);
            EditorGUILayout.Space(4);
            DrawLine();

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            foreach (var (name, desc, api) in Systems)
            {
                EditorGUILayout.Space(8);
                EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
                EditorGUILayout.LabelField(desc, EditorStyles.wordWrappedLabel);
                EditorGUILayout.Space(2);

                var style = new GUIStyle(EditorStyles.helpBox)
                {
                    font = EditorStyles.miniFont,
                    fontSize = 10,
                    wordWrap = false
                };
                EditorGUILayout.LabelField(api, style);
                DrawLine();
            }

            EditorGUILayout.EndScrollView();
        }

        private static void DrawLine()
        {
            var rect = EditorGUILayout.GetControlRect(false, 1);
            EditorGUI.DrawRect(rect, new Color(0.3f, 0.3f, 0.3f, 1f));
        }
    }
}
