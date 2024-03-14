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

/*       마우스가 아닌 키보드를 이용해서 캐릭터 이동하는 코드
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // 캐릭터 이동 속도

    void Update()
    {
        // 키 입력을 감지하여 이동 방향을 설정
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));  // horizontal은 좌우이동 vertiacl은 앞뒤 이동을 나타낸다.
        movementInput = Vector3.ClampMagnitude(movementInput, 1f); // 입력 벡터의 크기를 1로 제한하여 대각선 이동 속도를 유지

        // 캐릭터를 이동 방향으로 이동
        MoveCharacter(movementInput);
    }

    void MoveCharacter(Vector3 direction)
    {
        // 입력된 이동 방향이 존재하면
        if (direction != Vector3.zero)
        {
            // 이동 방향으로 이동하도록 설정
            Vector3 movement = direction * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
    }
}
*/