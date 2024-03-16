using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target; // 플레이어의 위치를 알아야 하기 때문에 Transform으로 선언

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}


