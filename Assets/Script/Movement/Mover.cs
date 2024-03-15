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
        // if(Input.GetMouseButton(0))   // 0은 왼쪽 1은 오른쪽 2는 중간버튼을 의미한다. , 마우스가 눌러져있는동안 true를 반환하고 싶으면 GetMouseButton으로 수정한다.
        // {
        //     MoveToCursor();
        // }
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   // 메인카메라를 lastray 로 가져온다.
        RaycastHit hit;       // 충돌 지점을 정보를 담고있는 구조체 (마우스를 클릭했을때)
        bool hasHit = Physics.Raycast(ray,out hit); //hasHit은 true 혹은 false를 반환
        if (hasHit == true)
        {
            MoveTo(hit.point);
        }
    }

    public void MoveTo(Vector3 destination)      // 메서드 추출 기능을 통해 새로운 메서드로 추출 , 외부에서 가져올수 있어야 하므로 public으로 변경
    {
        GetComponent<NavMeshAgent>().destination = destination; // hasHit이 true이면 내비메시 에이전트의 목적지를 레이캐스트 중돌지점으로 변경
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;    // NavmeshAgent에서 global velocity를 가져온다.
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);    // 캐릭터에 연결된 로컬변수로 변환한다.
        float speed = localVelocity.z;      // 전방으로 얼마나 빨리 움직이는지 알아보기 위해 z값으로 지정
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
         
    
}

/*      // 마우스가 아닌 키보드를 이용해서 움직이는 코드
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool hasMoved = Move();
        UpdateAnimator(hasMoved);
    }

    private bool Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);
        
        if (inputDirection.sqrMagnitude > 0.1f) // 입력이 있는지 감지하기 위해 약간의 임계값 설정
        {
            agent.destination = transform.position + inputDirection; // NavMeshAgent를 사용한 이동 설정
            return true;
        }
        return false;
    }
      
    private void UpdateAnimator(bool hasMoved)
    {
        if (hasMoved)
        {
            // 전방으로 얼마나 빨리 움직이는지 계산하여 forwardSpeed 설정
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float forwardSpeed = localVelocity.z;
            animator.SetFloat("forwardSpeed", forwardSpeed);
        }
        else
        {
            animator.SetFloat("forwardSpeed", 0);
        }
    }
}
*/