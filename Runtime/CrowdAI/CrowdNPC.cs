using UnityEngine;
using UnityEngine.AI;

namespace NpcMentality
{
    [AddComponentMenu("NPC Mentality/Crowd NPC")]
    [RequireComponent(typeof(NavMeshAgent))]
    public class CrowdNPC : MonoBehaviour
    {
        public NavMeshAgent Agent;
        public float WanderRadius = 10f;
        public float BehaviorChangeCooldown = 3f;

        public CrowdBehaviorType CurrentBehavior => _currentBehavior;

        private CrowdBehaviorType _currentBehavior = CrowdBehaviorType.Idle;
        private float _behaviorTimer;
        private float _actionTimer;
        private float _baseSpeed;

        private void Awake()
        {
            if (Agent == null)
                Agent = GetComponent<NavMeshAgent>();
            _baseSpeed = Agent.speed;
        }

        private void Update()
        {
            _behaviorTimer -= Time.deltaTime;
            if (_behaviorTimer <= 0f)
            {
                PickRandomBehavior();
                _behaviorTimer = BehaviorChangeCooldown;
            }

            _actionTimer -= Time.deltaTime;
            ExecuteBehavior();
        }

        private void PickRandomBehavior()
        {
            // exclude Hurt from random selection; it is set externally
            int roll = Random.Range(0, 6);
            _currentBehavior = (CrowdBehaviorType)roll; // maps 0-5 to Wander..AvoidObstacle
            _actionTimer = 0f; // reset so new behavior starts immediately
        }

        private void ExecuteBehavior()
        {
            switch (_currentBehavior)
            {
                case CrowdBehaviorType.Wander:
                    if (_actionTimer <= 0f)
                    {
                        Agent.speed = _baseSpeed;
                        Vector3 dest = GetRandomNavPoint(transform.position, WanderRadius);
                        Agent.SetDestination(dest);
                        _actionTimer = BehaviorChangeCooldown;
                    }
                    break;

                case CrowdBehaviorType.BrowseShop:
                    if (_actionTimer <= 0f)
                    {
                        Agent.speed = _baseSpeed * 0.5f;
                        GameObject shop = FindNearbyShop();
                        if (shop != null)
                        {
                            Agent.SetDestination(shop.transform.position);
                        }
                        else
                        {
                            Vector3 dest = GetRandomNavPoint(transform.position, WanderRadius * 0.5f);
                            Agent.SetDestination(dest);
                        }
                        _actionTimer = BehaviorChangeCooldown;
                    }
                    break;

                case CrowdBehaviorType.LookAround:
                    if (_actionTimer <= 0f)
                    {
                        Agent.ResetPath();
                        Agent.speed = 0f;
                        _actionTimer = Random.Range(1f, 3f);
                    }
                    else
                    {
                        // slowly rotate in place
                        transform.Rotate(Vector3.up, 30f * Time.deltaTime);
                    }
                    break;

                case CrowdBehaviorType.StopAndObserve:
                    if (_actionTimer <= 0f)
                    {
                        Agent.ResetPath();
                        Agent.speed = 0f;
                        _actionTimer = Random.Range(1f, 2f);
                    }
                    break;

                case CrowdBehaviorType.AvoidObstacle:
                    if (_actionTimer <= 0f)
                    {
                        Agent.speed = _baseSpeed;
                        // offset the current destination slightly to steer around obstacles
                        Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
                        Vector3 dest = GetRandomNavPoint(transform.position + offset, WanderRadius * 0.3f);
                        Agent.SetDestination(dest);
                        _actionTimer = BehaviorChangeCooldown * 0.5f;
                    }
                    break;

                case CrowdBehaviorType.Hurt:
                    // managed externally; just stop movement
                    Agent.ResetPath();
                    Agent.speed = 0f;
                    break;

                case CrowdBehaviorType.Idle:
                    Agent.ResetPath();
                    Agent.speed = 0f;
                    break;
            }
        }

        private Vector3 GetRandomNavPoint(Vector3 origin, float radius)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector3 randomPoint = origin + Random.insideUnitSphere * radius;
                randomPoint.y = origin.y;
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, radius, NavMesh.AllAreas))
                    return hit.position;
            }
            return origin;
        }

        private GameObject FindNearbyShop()
        {
            GameObject[] shops = GameObject.FindGameObjectsWithTag("Shop");
            if (shops == null || shops.Length == 0) return null;

            GameObject nearest = null;
            float nearestDist = float.MaxValue;
            foreach (var shop in shops)
            {
                float dist = Vector3.Distance(transform.position, shop.transform.position);
                if (dist < nearestDist && dist <= WanderRadius)
                {
                    nearestDist = dist;
                    nearest = shop;
                }
            }
            return nearest;
        }

        public void SetBehavior(CrowdBehaviorType behavior)
        {
            _currentBehavior = behavior;
            _actionTimer = 0f;
        }
    }
}
