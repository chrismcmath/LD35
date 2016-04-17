using UnityEngine;
using System.Collections;

namespace Rf.View.Controllers {
    public class CannonController : MonoBehaviour {
        void Start () {
            ShapeView.OnNewShape += OnNewShape;
        }

        void Update () {
        }

        private void OnNewShape() {
            Fire();
        }

        private void Fire() {
            GameObject instance = Instantiate(Resources.Load("game/lasers/laser1", typeof(GameObject))) as GameObject;
            instance.transform.position = transform.position;
            instance.transform.rotation = transform.rotation;
        }
    }
}
