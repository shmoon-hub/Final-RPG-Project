/////////////////////////////////////////////////
//   코드 소개                                 //
//   플레이어의 움직임을 나타내는 코드          //
//   레이캐스팅은 마우스를 클릭해서 움직임을    //
//   나타낼때 사용된다.                       //
////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
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
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)      // 메서드 추출 기능을 통해 새로운 메서드로 추출 , 외부에서 가져올수 있어야 하므로 public으로 변경
        {
            navMeshAgent.destination = destination; // hasHit이 true이면 내비메시 에이전트의 목적지를 레이캐스트 중돌지점으로 변경
            navMeshAgent.isStopped = false;
        }

        public void Stop()
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


// 마우스가 아닌 키보드를 이용해서 움직이는 코드 <새로 만든 코드>
// using System.Collections;
// using System.Collections.Generic;
// using RPG.Combat;
// using UnityEngine;
// using UnityEngine.AI;

// namespace RPG.Movement
// {
//     public class Mover : MonoBehaviour
//     {
//         [SerializeField] Transform target; // 이동 대상 지정

//         NavMeshAgent navMeshAgent;


//         private void Start()
//         {
//             navMeshAgent = GetComponent<NavMeshAgent>();
//         }

//         void Update()
//         {
//             UpdateAnimator();
//             MoveWithKeyboard(); // Update 메소드에서 MoveWithKeyboard 호출
//         }

//         public void StartMoveAction(Vector3 destination)
//         {
//             GetComponent<Fighter>().Cancel(); // Cancel 메소드의 올바른 철자 확인
//             MoveTo(destination);
//         }

//         public void MoveTo(Vector3 destination)
//         {
//             navMeshAgent.destination = destination;
//             navMeshAgent.isStopped = false;
//         }

//         public void Stop()
//         {
//             navMeshAgent.isStopped = true;
//         }

//         private void UpdateAnimator()
//         {
//             Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
//             Vector3 localVelocity = transform.InverseTransformDirection(velocity);
//             float speed = localVelocity.z;
//             GetComponent<Animator>().SetFloat("forwardSpeed", speed);
//         }

//         private bool isMovingWithKeyboard = false; // 키보드 이동 상태 추적

//         public void MoveWithKeyboard()
//         {

//             isMovingWithKeyboard = true; // 키보드로 이동 시작

//             float horizontal = Input.GetAxis("Horizontal");
//             float vertical = Input.GetAxis("Vertical");
//             Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized; // 입력 방향 정규화

//             if (inputDirection.sqrMagnitude > 0.01f)
//             {
//                 // 입력에 따라 캐릭터의 위치 조정
//                 navMeshAgent.destination = transform.position + inputDirection;
//                 navMeshAgent.isStopped = false; // 목적지로 계속 이동하도록 함
//             }
//         }

        
//     }
//  }

// 마우스가 아닌 키보드를 이용해서 움직이는 코드 <기존 코드>
// using RPG.Combat;
// using RPG.Control;
// using UnityEngine;
// using UnityEngine.AI;

// namespace RPG.Movement
// {
//     public class Mover : MonoBehaviour
//     {
//         private NavMeshAgent agent;

//         private void Awake()
//         {
//             agent = GetComponent<NavMeshAgent>();
//         }

//         Animator animator;
//         PlayerController playerController;

//         void Start()
//         {
//             animator = GetComponent<Animator>();
//             playerController = GetComponent<PlayerController>(); // PlayerController 컴포넌트를 가져옵니다.
//         }

//         void Update()
//         {
//             UpdateAnimator();
//         }

//         public void MoveWithKeyboard()
//         {
//             float horizontal = Input.GetAxis("Horizontal");
//             float vertical = Input.GetAxis("Vertical");
//             Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);

//             if (inputDirection.sqrMagnitude > 0.01f)
//             {
//                 agent.destination = transform.position + inputDirection;
//             }
//         }

//         private void UpdateAnimator()
//         {
//             // PlayerController에서 NavMeshAgent 접근자를 통해 velocity를 가져옵니다.
//             Vector3 velocity = playerController.Agent.velocity;
//             Vector3 localVelocity = transform.InverseTransformDirection(velocity);
//             float forwardSpeed = localVelocity.z;
//             animator.SetFloat("forwardSpeed", forwardSpeed);
//         }

//         public void StartMoveAction(Vector3 destination)     // 추가된 부분
//         {
//             GetComponent<Fighter>().Cancle();
//             MoveTo(destination);
//         }

//         // 지정된 위치로 이동하는 기능을 수행합니다.
//         public void MoveTo(Vector3 destination)
//         {
//             agent.destination = destination;
//             agent.isStopped = false;
//         }

//         // 캐릭터의 이동을 중지합니다.
//         public void Stop()
//         {
//             agent.isStopped = true;
//         }
//     }
// }



