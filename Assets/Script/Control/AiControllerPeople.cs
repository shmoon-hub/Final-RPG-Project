////////////////////////////////////
// 적 AI 컨트롤 하는 기능          //
//                                //
///////////////////////////////////
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
        // Unity 인스펙터에서 수정 가능하게 만들기 위해 선언
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;  // 의심 행동 대기시간
        [SerializeField] PatrolPath patrolPath;   // Patrol Path 구현
        [SerializeField] float waypointTolerance = 1f;   //적이 순찰지점에 도달했다고 간주되는 거리의 한계값
        [SerializeField] float waypointDwellTime = 3f;   // 웨이포인트에서 일정한 시간을 두고 캐릭터가 움직일수 있게 하는 변수

        FighterPeople fighterpeople;  //적이 공격을 수행할 수 있게 하는 컴포넌트
        Health health;   // 체력을 나타냄
        PeopleMover peopleMover;  // 움직임을 나타냄
        GameObject player;   // player 객체 -> Enemy 입장에서는 player가 적

        Vector3 guardPosition;  // 제자리로 돌아갈수 있도록 변수 설정
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start() {   // 게임이 시작될 때 컴포넌트들을 초기화하는 부분
            fighterpeople = GetComponent<FighterPeople>();
            health = GetComponent<Health>();
            peopleMover = GetComponent<PeopleMover>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
        }
        private void Update()   //매 프레임마다 적의 상태를 업데이트하는 부분
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighterpeople.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)   //순찰 경로가 설정되어 있고, 현재 위치가 순찰지점에 도달했다면 다음 순찰지점으로 이동
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                peopleMover.StartMoveAction(nextPosition);  //순찰 경로가 없거나 순찰지점이 더 이상 없다면, 초기 위치(guardLocation)로 이동
            }
        }

        private bool AtWaypoint()  // 현재 위치가 현재 순찰지점에 도달했는지 확인
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }
        private void CycleWaypoint()   // 다음 순찰지점으로 인덱스를 업데이트
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }
        private Vector3 GetCurrentWaypoint()  // 현재 순찰지점의 위치를 반환
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }
        private void SuspicionBehaviour()   // 현재 수행 중인 모든 액션을 취소하고, 적을 의심 상태로 만듦
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()  // Fighter 컴포넌트의 Attack 메서드를 호출하여 플레이어를 공격
        {
            timeSinceLastSawPlayer = 0;
            fighterpeople.Attack(player);
        }

        private bool InAttackRangeOfPlayer()  // 적이 플레이어를 공격할지 여부를 결정하는 데 사용
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }


        //  유니티에서 제공하는 기즈모 기능 => 상세설명은 unity docs 참조
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;      // 기즈모에 사용할 색깔을 지정 (파랑색)
            Gizmos.DrawWireSphere(transform.position, chaseDistance);  // 기즈모에 사용할 모양 설정 및 기즈모 중심 좌표 및 반경 설정
        }
    }
}
