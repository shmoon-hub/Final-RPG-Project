using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour {
          GameObject player;

        private void Start() {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = GameObject.FindWithTag("Player");
        }

        void DisableControl(PlayableDirector pd)
        {
            //print("DisableControl");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            //print("EnableControl");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
