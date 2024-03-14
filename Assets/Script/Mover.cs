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
        UpdateAnimator();
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
      
    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;    // NavmeshAgent에서 global velocity를 가져온다.
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);    // 캐릭터에 연결된 로컬변수로 변환한다.
        float speed = localVelocity.z;      // 전방으로 얼마나 빨리 움직이는지 알아보기 위해 z값으로 지정
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
         
    
}

/*         // 마우스가 아닌 키보드를 이용해서 움직이도록 함 -> 아직 navmesh agent는 적용 안됨
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // 캐릭터 이동 속도

    private NavMeshAgent navMeshAgent; // NavMeshAgent 컴포넌트를 저장할 변수

    void Start()
    {
        // NavMeshAgent 컴포넌트를 가져와 변수에 저장
        navMeshAgent = GetComponent<NavMeshAgent>();
        // 초기 속도 설정
        navMeshAgent.speed = moveSpeed;
    }

    void Update()
    {
        // 키 입력을 감지하여 이동 방향을 설정
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        movementInput = Vector3.ClampMagnitude(movementInput, 1f); // 입력 벡터의 크기를 1로 제한하여 대각선 이동 속도를 유지

        // 캐릭터를 이동 방향으로 이동
        MoveCharacter(movementInput);
    }

    void MoveCharacter(Vector3 direction)
    {
        // 입력된 이동 방향이 존재하면
        if (direction != Vector3.zero)
        {
            // NavMeshAgent의 이동 방향 설정
            navMeshAgent.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
}
*/