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
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;      // 올바른 위치에 캐릭터가 있으면 true 반환
            print("Nothing to do");
        }

        private bool InteractWithCombat()        // 부울린 반환
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
                return true;
            }
            return false;
            //throw new NotImplementedException();
        }

        private bool InteractWithMovement()
        {
            
            RaycastHit hit;       // 충돌 지점을 정보를 담고있는 구조체 (마우스를 클릭했을때)
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); //hasHit은 true 혹은 false를 반환
            if (hasHit == true)
            {
                if(Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
                return true;
            }
            return false;
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
//         public NavMeshAgent Agent => agent;

//         private NavMeshAgent agent;
//         private Fighter fighter;

//         private void Awake()
//         {
//             agent = GetComponent<NavMeshAgent>();
//             fighter = GetComponent<Fighter>(); // Fighter 컴포넌트를 가져옵니다.
//         }

//         private void Update()
//         {
//             if (InteractWithCombat()) return;
//             if (InteractWithMovement()) return;
//             print("Nothing to do!");
//         }

//         private bool InteractWithCombat()
//         {
//             RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
//             foreach (RaycastHit hit in hits)
//             {
//                 CombatTarget target = hit.transform.GetComponent<CombatTarget>();
//                 if (target == null) continue;
                
//                 if (Input.GetMouseButtonDown(0))
//                 {
//                     fighter.Attack(target);
//                     return true;
//                 }
//             }
//             return false;
//         }

//         private bool InteractWithMovement()
//         {
//             if (Input.GetMouseButton(0))
//             {
//                 if (MoveToCursor()) // Raycast가 히트했다면
//                 {
//                     return true;
//                 }
//             }
//             else
//             {
//                 GetComponent<Mover>().MoveWithKeyboard();
//                 return true;
//             }
//             return false; // 마우스 클릭 위치가 맵 내부가 아닐 경우
//         }

//         private bool MoveToCursor() // bool 타입을 반환하도록 수정
//         {
//             RaycastHit hit;
//             bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
//             if (hasHit)
//             {
//                 agent.destination = hit.point;
//                 return true; // 히트 성공
//             }
//             return false; // 히트 실패
//         }

//         private static Ray GetMouseRay()
//         {
//             return Camera.main.ScreenPointToRay(Input.mousePosition);
//         }
//     }
// }
