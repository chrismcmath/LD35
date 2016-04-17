using UnityEngine;
using System.Collections;

namespace Rf.View.Controllers {
    public class PocketRimController : MonoBehaviour {
        public const float FORCE = 10f;
        private void OnTriggerStay2D(Collider2D other) {
            Debug.LogFormat(" [Rim] OnTriggerStay {0}", other.name);
            Rigidbody2D r2D = other.gameObject.GetComponentInParent<Rigidbody2D>();
            if (r2D != null) {
                r2D.AddForce((transform.position - other.transform.position) * FORCE);
            }
        }
    }
}
