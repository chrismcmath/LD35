using UnityEngine;
using System.Collections;

namespace Rf.View.Controllers {
    public class PocketGoalController : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            other.gameObject.GetComponentInParent<LaserController>().Pot(transform.position);
        }

        /*
        private void OnTriggerStay2D(Collider2D other) {
            Debug.LogFormat(" [Goal] OnTriggerStay {0}", other.name);
        }

        private void OnTriggerExit2D(Collider2D other) {
            Debug.LogFormat(" [Goal] OnTriggerExit2D");
        }
        */
    }
}
