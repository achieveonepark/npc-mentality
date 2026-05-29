using UnityEngine;
using UnityEngine.AI;

namespace NpcMentality
{
    [AddComponentMenu("NPC Mentality/Emotion Controller")]
    public class EmotionController : MonoBehaviour
    {
        public Animator Animator;
        public NavMeshAgent Agent;
        public EmotionType CurrentEmotion { get; private set; } = EmotionType.Neutral;

        private float _baseSpeed;

        private void Awake()
        {
            if (Agent == null)
                Agent = GetComponent<NavMeshAgent>();

            if (Agent != null)
                _baseSpeed = Agent.speed;
        }

        public void Set(EmotionType emotion)
        {
            CurrentEmotion = emotion;
            ApplyEmotionEffects();
        }

        private void ApplyEmotionEffects()
        {
            switch (CurrentEmotion)
            {
                case EmotionType.Angry:
                    SetAgentSpeed(1.5f);
                    TriggerAnimation("Angry");
                    SetBlinkRate(0.3f);
                    break;

                case EmotionType.Happy:
                    SetAgentSpeed(1.0f);
                    TriggerAnimation("Happy");
                    SetBlinkRate(1.0f);
                    break;

                case EmotionType.Sad:
                    SetAgentSpeed(0.7f);
                    TriggerAnimation("Sad");
                    SetBlinkRate(0.8f);
                    break;

                case EmotionType.Afraid:
                    SetAgentSpeed(1.3f);
                    TriggerAnimation("Afraid");
                    SetBlinkRate(1.5f);
                    break;

                case EmotionType.Surprised:
                    SetAgentSpeed(1.0f);
                    TriggerAnimation("Surprised");
                    SetBlinkRate(1.2f);
                    break;

                case EmotionType.Disgusted:
                    SetAgentSpeed(0.8f);
                    TriggerAnimation("Disgusted");
                    SetBlinkRate(0.6f);
                    break;

                case EmotionType.Neutral:
                default:
                    ResetAgentSpeed();
                    TriggerAnimation("Idle");
                    SetBlinkRate(1.0f);
                    break;
            }
        }

        private void SetAgentSpeed(float multiplier)
        {
            if (Agent != null)
                Agent.speed = _baseSpeed * multiplier;
        }

        private void ResetAgentSpeed()
        {
            if (Agent != null)
                Agent.speed = _baseSpeed;
        }

        private void TriggerAnimation(string triggerName)
        {
            if (Animator != null)
                Animator.SetTrigger(triggerName);
        }

        private void SetBlinkRate(float rate)
        {
            if (Animator != null)
                Animator.SetFloat("BlinkRate", rate);
        }
    }
}
