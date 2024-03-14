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

    Ray lastRay; // 레이캐스팅 구현을 위해 레이 타입의 변수 생성

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))   // 0은 왼쪽 1은 오른쪽 2는 중간버튼을 의미한다.
        {
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);   // 메인카메라를 lastray 로 가져온다.
        }
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);         // 100을 곱하는 이유는 먼거리까지 Ray가 그려질 것이기 때문이다.

        GetComponent<NavMeshAgent>().destination = target.position;  // 타겟으로 이동하게끔 네비메쉬 설정
    }
}
