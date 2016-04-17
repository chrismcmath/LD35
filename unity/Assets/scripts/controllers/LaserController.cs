using UnityEngine;
using System.Collections;

namespace Rf.View.Controllers {
    public class LaserController : MonoBehaviour {
        public Transform Head;
        public Transform Tail;

        private LayerMask _Mask;
        private Rigidbody2D _Rigidbody;

        public void Start() {
            _Mask = 1 << LayerMask.NameToLayer("Shape");
            _Rigidbody = GetComponent<Rigidbody2D>();

            Reboost(10f);
        }

        public void Pot(Vector2 position) {
            Destroy(gameObject);
        }

        private void OnCollisionExit2D(Collision2D coll) {
            transform.up = _Rigidbody.velocity.normalized;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (LayerMask.LayerToName(other.gameObject.layer) != "Shape") return;
            Debug.LogFormat("OnTriggerEnter2D, layer: {0}", LayerMask.LayerToName(other.gameObject.layer));
            Raycast(Tail);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (LayerMask.LayerToName(other.gameObject.layer) != "Shape") return;
            Debug.LogFormat("OnTriggerExit2D, layer: {0}", LayerMask.LayerToName(other.gameObject.layer));
            Raycast(Head, false);
        }

        private void Raycast(Transform from, bool forward = true) {
            Vector2 direction = from.up;
            if (!forward) {
                direction *= -1;
            }
            RaycastHit2D hit = Physics2D.Raycast(from.position, direction, Mathf.Infinity, _Mask); 
            if (hit != null && hit.transform != null) {
                float incidence = AngleSigned(hit.normal, direction * -1);
                Debug.LogFormat("hit: {0}", hit.transform);
                Debug.LogFormat("Hit: {0}, Normal: {1}, Direction: {2}, Incidence: {3}",
                        hit.transform.name,
                        hit.normal,
                        direction * -1,
                        incidence);

                Refract(incidence, hit.normal, forward);
            }
        }

        private void OnNewShape() {
            Fire();
        }

        private void Refract(float incidence, Vector2 normal, bool forward) {
            float snell;
            if (forward) {
                snell = Snell(1f, incidence, 1.5f);
            } else {
                snell = Snell(1.5f, incidence, 1.0f);
            }

            if (forward) {
                normal *= -1;
            } 

            float delay = 50f;
            Debug.DrawRay(transform.position, transform.up, Color.red, delay);

            transform.up = Quaternion.Euler(0, 0, (snell)) * normal;
            Reboost();

            Debug.DrawRay(transform.position, normal, Color.blue, delay);
            Debug.DrawRay(transform.position, normal * -1, Color.blue, delay);
            Debug.DrawRay(transform.position, transform.up, Color.yellow, delay);
        }

        private float Snell(float incidentIndex, float incidenceAngle, float refractiveIndex) {
            //NOTE: Hack to stop total internal reflection
            float lhs = Mathf.Min(1f, incidentIndex * Mathf.Sin(incidenceAngle * Mathf.Deg2Rad));
            float divided = lhs / refractiveIndex;
            float asin =  Mathf.Asin(divided);
            return asin * Mathf.Rad2Deg;
        }

        private void Fire() {
            GameObject instance = Instantiate(Resources.Load("game/lasers/laser1", typeof(GameObject))) as GameObject;
        }

        private void Reboost(float speed = 0f) {
            if (speed == 0f) {
                speed = _Rigidbody.velocity.magnitude;
            }
            _Rigidbody.velocity = Vector2.zero;
            _Rigidbody.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        public float AngleSigned(Vector3 v1, Vector3 v2) {
            Vector3 n =  new Vector3(0, 0, 1f);
            return Mathf.Atan2(
                    Vector3.Dot(n, Vector3.Cross(v1, v2)),
                    Vector3.Dot(v1, v2)
                ) * Mathf.Rad2Deg;
        }
    }
}
