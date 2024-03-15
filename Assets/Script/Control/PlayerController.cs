using UnityEngine;

public class PlayerController : MonoBehaviour {

    private void Update() {
        if(Input.GetMouseButton(0))   // 0은 왼쪽 1은 오른쪽 2는 중간버튼을 의미한다. , 마우스가 눌러져있는동안 true를 반환하고 싶으면 GetMouseButton으로 수정한다.
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
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}



//키보드를 이용해서 움직이는 부분
// using UnityEngine;
// using UnityEngine.AI;

// public class PlayerController : MonoBehaviour
// {
//     private NavMeshAgent agent;
//      public NavMeshAgent Agent => agent; // NavMeshAgent에 대한 공개 접근자 추가

//     private void Awake()
//     {
//         agent = GetComponent<NavMeshAgent>();
//     }

//     public void Move()
//     {
//         float horizontal = Input.GetAxis("Horizontal");
//         float vertical = Input.GetAxis("Vertical");
//         Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);
        
//         if (inputDirection.sqrMagnitude > 0.1f) // 입력이 있는지 감지하기 위해 약간의 임계값 설정
//         {
//             agent.destination = transform.position + inputDirection; // NavMeshAgent를 사용한 이동 설정
//         }
//     }
// }

