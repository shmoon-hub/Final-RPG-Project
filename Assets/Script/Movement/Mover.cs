/////////////////////////////////////////////////
//   코드 소개                                 //
//   플레이어의 움직임을 나타내는 코드          //
//   레이캐스팅은 마우스를 클릭해서 움직임을    //
//   나타낼때 사용된다.                       //
////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target; // 이동 타겟 지정

        // Ray lastRay;  레이캐스팅 구현을 위해 레이 타입의 변수 생성

        void Update()
        {

            UpdateAnimator();
        }



        public void MoveTo(Vector3 destination)      // 메서드 추출 기능을 통해 새로운 메서드로 추출 , 외부에서 가져올수 있어야 하므로 public으로 변경
        {
            GetComponent<NavMeshAgent>().destination = destination; // hasHit이 true이면 내비메시 에이전트의 목적지를 레이캐스트 중돌지점으로 변경
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


// 마우스가 아닌 키보드를 이용해서 움직이는 코드
// using RPG.Control;
// using UnityEngine;
// using UnityEngine.AI;

// namespace RPG.Movement
// {
//     public class Mover : MonoBehaviour
//     {
//         Animator animator;
//         PlayerController playerController;

//         void Start()
//         {
//             animator = GetComponent<Animator>();
//             playerController = GetComponent<PlayerController>(); // PlayerController 컴포넌트를 가져옵니다.
//         }

//         void Update()
//         {
//             playerController.Move(); // PlayerController의 Move 메서드를 호출합니다.
//             UpdateAnimator();
//         }
        
//         private void UpdateAnimator()
//         {
//             Vector3 velocity = playerController.Agent.velocity; // agent.velocity 대신 사용
//             Vector3 localVelocity = transform.InverseTransformDirection(velocity);
//             float forwardSpeed = localVelocity.z;
//             animator.SetFloat("forwardSpeed", forwardSpeed);
//         }
//     }
// }


