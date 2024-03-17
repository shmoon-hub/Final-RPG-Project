// 기존 소스코드
using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour {

    private void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
            //throw new NotImplementedException();
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))   // 0은 왼쪽 1은 오른쪽 2는 중간버튼을 의미한다. , 마우스가 눌러져있는동안 true를 반환하고 싶으면 GetMouseButton으로 수정한다.
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;       // 충돌 지점을 정보를 담고있는 구조체 (마우스를 클릭했을때)
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); //hasHit은 true 혹은 false를 반환
            if (hasHit == true)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
            // 메인카메라를 lastray 로 가져온다.
        }
    }
}


// 키보드로 이동하고 마우스로 적을 공격하는 부분
// using UnityEngine;
// using UnityEngine.AI;
// using RPG.Movement;
// using RPG.Combat;

// namespace RPG.Control
// {
//     public class PlayerController : MonoBehaviour
//     {
//         public NavMeshAgent Agent => agent; // 기존 코드에 이 줄 추가

//         private NavMeshAgent agent;

//         private void Awake()
//         {
//             agent = GetComponent<NavMeshAgent>();
//         }

//         private void Update()
//         {
//             if (InteractWithCombat()) return; // 공격 인터랙션에 성공하면 이동 인터랙션을 처리하지 않음
//             InteractWithMovement();
//         }

//         private bool InteractWithCombat()
//         {
//             RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
//             foreach (RaycastHit hit in hits)
//             {
//                 CombatTarget target = hit.transform.GetComponent<CombatTarget>();
//                 if (target == null) continue;
                
//                 if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
//                 {
//                     GetComponent<Fighter>().Attack(target);
//                     return true; // 공격 인터랙션이 처리되었음을 나타냄
//                 }
//             }
//             return false; // 공격 인터랙션이 처리되지 않음
//         }

//         private void InteractWithMovement()
//         {
//             if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼을 계속 누르고 있을 때
//             {
//                 MoveToCursor();
//             }
//             else
//             {
//                 MoveWithKeyboard(); // 키보드 입력으로 이동 처리
//             }
//         }

//         private void MoveToCursor()
//         {
//             RaycastHit hit;
//             bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
//             if (hasHit)
//             {
//                 agent.destination = hit.point;
//             }
//         }

//         private void MoveWithKeyboard()
//         {
//             float horizontal = Input.GetAxis("Horizontal");
//             float vertical = Input.GetAxis("Vertical");
//             Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);
            
//             if (inputDirection.sqrMagnitude > 0.01f) // 입력 감지 시
//             {
//                 agent.destination = transform.position + inputDirection; // NavMeshAgent를 이용하여 이동
//             }
//         }

//         private static Ray GetMouseRay()
//         {
//             return Camera.main.ScreenPointToRay(Input.mousePosition);
//         }
//     }
// }
