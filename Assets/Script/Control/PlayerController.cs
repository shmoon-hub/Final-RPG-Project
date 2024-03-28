///////////////////////////////////////
//     코드 소개                     //
//                                  //
// Mover.cs에 있는 움직임 정보를 가  //
// 져와서 Enemy컴포넌트를 클릭했을   //
// 때 움직임을 구현함               //
////////////////////////////////////

// 기존 소스코드
// using System;
// using RPG.Combat;
// using RPG.Movement;
// using UnityEngine;
// using RPG.Core;

// namespace RPG.Control
// {
//     public class PlayerController : MonoBehaviour {

//     [SerializeField] float moveSpeed = 5f; // 필요에 따라 이동 속도 조절

//     private void Update()
//         {
//             if (InteractWithCombat()) return;
//             if (InteractWithMovement()) return;      // 올바른 위치에 캐릭터가 있으면 true 반환
//             print("Nothing to do");
//         }

//          private bool InteractWithCombat()
//         {
//             RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
//             foreach (RaycastHit hit in hits)
//             {
//                 CombatTarget target = hit.transform.GetComponent<CombatTarget>();
//                 if (target == null) continue;
//                 if (!GetComponent<Fighter>().CanAttack(target.gameObject))
//                 {
//                     continue;
//                 }
//                 if (Input.GetMouseButton(0))
//                 {
//                     GetComponent<Fighter>().Attack(target.gameObject);
//                 }
//                 return true;
//             }
//             return false;
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

//         private static Ray GetMouseRay()
//         {
//             return Camera.main.ScreenPointToRay(Input.mousePosition);
//             // 메인카메라를 lastray 로 가져온다.
//         }

        
//     }
// }

// // 키보드로 이동하고 마우스로 적을 공격하는 부분 <새로 만든 코드>
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

            // 마우스 입력이 없을 때 키보드 입력에 의해 캐릭터를 움직입니다.
            MoveWithKeyboard(); 
        }

        private bool InteractWithCombat()        // 부울린 반환
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                GameObject targetGameObject = target.gameObject;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                    
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

        private void MoveWithKeyboard()
        {
            // 키보드 입력에 의한 캐릭터 이동 로직을 여기에 추가합니다.
            GetComponent<Mover>().MoveWithKeyboard();
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
            // 메인카메라를 lastray 로 가져온다.
        }
    }
}


