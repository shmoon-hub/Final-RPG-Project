using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        Health health;
        GameObject player;

        private void Start() {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }
        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //  유니티에서 제공하는 기즈모 기능 => 상세설명은 unity docs 참조
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;     // 기즈모에 사용할 색깔을 지정 (파랑색)
            Gizmos.DrawWireSphere(transform.position, chaseDistance);     // 기즈모에 사용할 모양 설정 및 기즈모 중심 좌표 및 반경 설정
        }
    }
}