/////////////////////////////////////////////////
//   코드 소개                                 //
//   플레이어의 움직임을 나타내는 코드          //
//   레이캐스팅은 마우스를 클릭해서 움직임을    //
//   나타낼때 사용된다.                       //
////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
       [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent navMeshAgent;
        Health health;

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }


    }
}


// 마우스가 아닌 키보드를 이용해서 움직이는 코드 <새로 만든 코드>
// using System.Collections;
// using System.Collections.Generic;
// using RPG.Combat;
// using RPG.Core;
// using UnityEngine;
// using UnityEngine.AI;

// namespace RPG.Movement
// {
//     public class Mover : MonoBehaviour, IAction
//     {
//         [SerializeField] Transform target; // 이동 대상 지정

//         [SerializeField] float maxSpeed = 6f;  // 최대 속도 6으로 설정

//         NavMeshAgent navMeshAgent;

//         Health health;


//         private void Start()
//         {
//             health = GetComponent<Health>();
//             navMeshAgent = GetComponent<NavMeshAgent>();
//         }

//         void Update()
//         {
//             navMeshAgent.enabled = !health.IsDead();
//             UpdateAnimator();
//             MoveWithKeyboard(); // Update 메소드에서 MoveWithKeyboard 호출
//         }

//         public void StartMoveAction(Vector3 destination, float speedFraction)
//         {
//             GetComponent<ActionScheduler>().StartAction(this);
//             //GetComponent<Fighter>().Cancel(); // Cancel 메소드의 올바른 철자 확인
//             MoveTo(destination, speedFraction);
//         }

//         public void MoveTo(Vector3 destination, float speedFraction)
//         {
//             navMeshAgent.destination = destination;
//             navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
//             navMeshAgent.isStopped = false;
//         }

//         public void Cancel()
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


