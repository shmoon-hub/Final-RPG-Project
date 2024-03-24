///////////////////////////////////////
//     코드 소개                     //
//                                  //
// Mover.cs에 있는 움직임 정보를 가  //
// 져와서 Enemy컴포넌트를 클릭했을   //
// 때 움직임을 구현함               //
////////////////////////////////////

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
                // 수정된 부분
                if (!GetComponent<Fighter>().CanAttack(target))
                {
                    continue;
                }
                
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
                    GetComponent<Mover>().StartMoveAction(hit.point);     // MoveTo를 StartMoveAction으로 바꿈
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

// 키보드로 이동하고 마우스로 적을 공격하는 부분 <새로 만든 코드>
// using System;
// using RPG.Combat;
// using RPG.Movement;
// using UnityEngine;

// namespace RPG.Control
// {
//     public class PlayerController : MonoBehaviour {

//     private void Update()
//         {
//             if (InteractWithCombat()) return;
//             if (InteractWithMovement()) return;      // 올바른 위치에 캐릭터가 있으면 true 반환
//             print("Nothing to do");

//             // 마우스 입력이 없을 때 키보드 입력에 의해 캐릭터를 움직입니다.
//             MoveWithKeyboard(); 
//         }

//         private bool InteractWithCombat()        // 부울린 반환
//         {
//             RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
//             foreach (RaycastHit hit in hits)
//             {
//                 CombatTarget target = hit.transform.GetComponent<CombatTarget>();
//                 if (target == null) continue;
                
//                 if (Input.GetMouseButtonDown(0))
//                 {
//                     GetComponent<Fighter>().Attack(target);
                    
//                 }
//                 return true;
//             }
//             return false;
//             //throw new NotImplementedException();
//         }

//         private bool InteractWithMovement()
//         {
            
//             RaycastHit hit;       // 충돌 지점을 정보를 담고있는 구조체 (마우스를 클릭했을때)
//             bool hasHit = Physics.Raycast(GetMouseRay(), out hit); //hasHit은 true 혹은 false를 반환
//             if (hasHit == true)
//             {
//                 if(Input.GetMouseButton(0))
//                 {
//                     GetComponent<Mover>().StartMoveAction(hit.point);     // MoveTo를 StartMoveAction으로 바꿈
//                 }
//                 return true;
//             }
//             return false;
//         }

//         private void MoveWithKeyboard()
//         {
//             // 키보드 입력에 의한 캐릭터 이동 로직을 여기에 추가합니다.
//             GetComponent<Mover>().MoveWithKeyboard();
//         }

//         private static Ray GetMouseRay()
//         {
//             return Camera.main.ScreenPointToRay(Input.mousePosition);
//             // 메인카메라를 lastray 로 가져온다.
//         }
//     }
// }


// 키보드로 이동하고 마우스로 적을 공격하는 부분 <기존 코드>
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
//             // 마우스 입력에 의한 상호작용이 있을 경우 키보드 입력으로 움직이지 않습니다.
//             if (InteractWithCombat()) return;
//             if (InteractWithMovement()) return;

//             // 마우스 입력이 없을 때 키보드 입력에 의해 캐릭터를 움직입니다.
//             MoveWithKeyboard();
//         }

//         private bool InteractWithCombat()
//         {
//             RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
//             foreach (RaycastHit hit in hits)
//             {
//                 CombatTarget target = hit.transform.GetComponent<CombatTarget>();
//                 if (target != null && Input.GetMouseButtonDown(0))
//                 {
//                     fighter.Attack(target);
//                     return true; // 적을 클릭하면 Attack을 호출하고 true를 반환합니다.
//                 }
//             }
//             return false; // 적을 클릭하지 않았다면 false를 반환합니다.
//         }

//         private bool InteractWithMovement()
//         {
//             if (!Input.GetMouseButton(0)) return false; // 마우스 왼쪽 버튼이 클릭되지 않았다면 바로 반환합니다.

//             RaycastHit hit;
//             bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
//             if (hasHit)
//             {
//                 GetComponent<Mover>().StartMoveAction(hit.point); // 마우스로 클릭한 위치로 이동합니다.
//                 return true;
//             }
//             return false; // 마우스 클릭 위치가 맵 내부가 아닐 경우 false를 반환합니다.
//         }

//         private void MoveWithKeyboard()
//         {
//             // 키보드 입력에 의한 캐릭터 이동 로직을 여기에 추가합니다.
//             GetComponent<Mover>().MoveWithKeyboard();
//         }

//         private static Ray GetMouseRay()
//         {
//             return Camera.main.ScreenPointToRay(Input.mousePosition);
//         }
//     }
// }
