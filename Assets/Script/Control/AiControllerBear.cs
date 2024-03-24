using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class AiControllerBear : MonoBehaviour {
         [SerializeField] float chaseDistance = 5f;

        FighterBear fighterBear;
        GameObject player;

        private void Start() {
            fighterBear = GetComponent<FighterBear>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (InAttackRangeOfPlayer() && fighterBear.CanAttack(player))
            {
                fighterBear.Attack(player);
            }
            else
            {
                //print(gameObject.name + " Should chase");
                fighterBear.Cancel();
            }
        }


        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}

