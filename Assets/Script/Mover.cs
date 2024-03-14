/////////////////////////////////////////////////
//   코드 소개                                 //
//   플레이어의 움직임을 나타내는 코드          //
//   레이캐스팅은 마우스를 클릭해서 움직임을    //
//   나타낼때 사용된다.                       //
////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       // navMeshAgent를 사용하기 위해 import

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target; // 이동 타겟 지정

    // Ray lastRay;  레이캐스팅 구현을 위해 레이 타입의 변수 생성

    void Update()
    {
        if(Input.GetMouseButtonDown(0))   // 0은 왼쪽 1은 오른쪽 2는 중간버튼을 의미한다.
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   // 메인카메라를 lastray 로 가져온다.
        RaycastHit hit;       // 충돌 지점을 정보를 담고있는 구조체 (마우스를 클릭했을때)
        bool hasHit = Physics.Raycast(ray,out hit); //hasHit은 true 혹은 false를 반환
        if (hasHit == true)
        {
            GetComponent<NavMeshAgent>().destination = hit.point; // hasHit이 true이면 내비메시 에이전트의 목적지를 레이캐스트 중돌지점으로 변경
        }
    }
        //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);         // 100을 곱하는 이유는 먼거리까지 Ray가 그려질 것이기 때문이다.

         
    
}
