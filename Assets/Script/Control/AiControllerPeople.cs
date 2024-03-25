using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class AiControllerPeople : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        FighterPeople fighterpeople;
        GameObject player;

        private void Start() {
            fighterpeople = GetComponent<FighterPeople>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (InAttackRangeOfPlayer() && fighterpeople.CanAttack(player))
            {
                fighterpeople.Attack(player);
            }
            else
            {
                //print(gameObject.name + " Should chase");
                fighterpeople.Cancel();
            }
        }


        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}
