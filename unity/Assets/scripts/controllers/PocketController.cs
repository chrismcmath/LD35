using UnityEngine;
using System.Collections;

namespace Rf.View.Controllers {
    public class PocketController : MonoBehaviour {
        private Rigidbody2D _Rigidbody;

        public void Start() {
            _Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D coll) {
            Debug.LogFormat(" [Pocket] OnCollisionEnter2D");
        }

        private void OnCollisionExit2D(Collision2D coll) {
            Debug.LogFormat(" [Pocket] OnCollisionExit2D");
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Debug.LogFormat(" [Pocket] OnTriggerEnter2D");
        }

        private void OnTriggerStay2D(Collider2D other) {
            //Debug.LogFormat(" [Pocket] OnTriggerStay {0}", other.name);
        }

        private void OnTriggerExit2D(Collider2D other) {
            Debug.LogFormat(" [Pocket] OnTriggerExit2D");
        }
    }
}
