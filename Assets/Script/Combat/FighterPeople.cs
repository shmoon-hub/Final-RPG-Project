using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat
{
    public class FighterPeople : MonoBehaviour , IAction
    {
         [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<PeopleMover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<PeopleMover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);   // 유니티 문서에서 지원하는 기능, 적을 향해 서도록 회전하는 기능
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if (target == null)
            {
                return;
            }
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) 
            {
                return false; 
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null &&!targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            stopAttack();
            target = null;
            GetComponent<PeopleMover>().Cancel();   // 기존 Mover
        }

        private void stopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
