/////////////////////////////////////////////
// 플레이어와 적의 체력을 나타내는 소스코드  //
////////////////////////////////////////////
using System;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;       // 플레이어 체력을 100으로 설정

        bool isDead = false;      // 초깃값을 false로 설정

        public bool IsDead()       // 만약 플레이어가 죽었을 경우      
        {
            return isDead;      // false return 
        }

        public void TakeDamage(float damage)          // 데미지를 가했을때 체력이 떨어지도록 함
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()                 // 만약 IsDead 메소드가 호출되면 애니메이터에 있는 die Trigger를 활성화하여 죽는 모션을 연출하고 죽음
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}