using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class AnimalMover : MonoBehaviour ,IAction
    {
        [SerializeField] Transform target; // 이동 타겟 지정

        NavMeshAgent navMeshAgent;


        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Ray lastRay;  레이캐스팅 구현을 위해 레이 타입의 변수 생성

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)     // 추가된 부분
        {
            GetComponent<ActionScheduler>().StartAction(this);
            //GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)      // 메서드 추출 기능을 통해 새로운 메서드로 추출 , 외부에서 가져올수 있어야 하므로 public으로 변경
        {
            navMeshAgent.destination = destination; // hasHit이 true이면 내비메시 에이전트의 목적지를 레이캐스트 중돌지점으로 변경
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;    // NavmeshAgent에서 global velocity를 가져온다.
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);    // 캐릭터에 연결된 로컬변수로 변환한다.
            float speed = localVelocity.z;      // 전방으로 얼마나 빨리 움직이는지 알아보기 위해 z값으로 지정
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
