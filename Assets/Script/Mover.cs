/////////////////////////////////////////////////
//   코드 소개                                 //
//   플레이어의 움직임을 나타내는 코드


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       // navMeshAgent를 사용하기 위해 import

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = target.position;
    }
}
