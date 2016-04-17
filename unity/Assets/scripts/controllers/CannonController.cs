using UnityEngine;
using System.Collections;

using Rf.View;

namespace Rf.Controllers {
    public class CannonController : MonoBehaviour {
        public BallController Ball;
        public AudioSource PullBack;
        public AudioSource Thrust;
        public float Speed = 10f;

        private Animator _Animator;

        void Start () {
            ShapeView.OnNewShape += OnNewShape;
            _Animator = GetComponent<Animator>();
        }

        void OnDestroy() {
            ShapeView.OnNewShape -= OnNewShape;
        }

        void Update () {
        }

        private void OnNewShape() {
            Fire();
        }

        private void Fire() {
            PullBack.Play();
            _Animator.Play("strike");
        }

        public void ThrustForward() {
            Thrust.Play();
            Ball.Strike(transform.up, Speed);
        }

        public void HitBall() {
            Ball.Strike(transform.up, Speed);
        }
    }
}
