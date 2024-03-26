using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AiControllerPeople : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        [SerializeField] float suspicionTime = 3f; // 의심 행동 대기시간

        [SerializeField] PatrolPath patrolPath;    // Patrol Path 구현

        [SerializeField] float waypointTolerance = 1f;

        FighterPeople fighterpeople;

        PeopleMover peopleMover;
        Health health;
        GameObject player;

        Vector3 guardLocation;

        float timeSinceLastSawPlayer = Mathf.Infinity;

        int currentWaypointIndex = 0;

        private void Start() {
            fighterpeople = GetComponent<FighterPeople>();
            health = GetComponent<Health>();
            peopleMover = GetComponent<PeopleMover>();
            player = GameObject.FindWithTag("Player");

            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighterpeople.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer <suspicionTime)
            {
                suspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardLocation;

            if (patrolPath != null)
            {
                if(Atwaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            peopleMover.StartMoveAction(nextPosition);
        }

        private bool Atwaypoint()
        {
            float distanceToWaypoint  = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void suspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighterpeople.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

         //  유니티에서 제공하는 기즈모 기능 => 상세설명은 unity docs 참조
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;     // 기즈모에 사용할 색깔을 지정 (파랑색)
            Gizmos.DrawWireSphere(transform.position, chaseDistance);     // 기즈모에 사용할 모양 설정 및 기즈모 중심 좌표 및 반경 설정
        }
    }
}
