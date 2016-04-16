using UnityEngine;
using System;
using System.Collections;

namespace Rf.Controls {
    public class DrawControls : MonoBehaviour {
        private bool _Drawing = false;

        public void Awake() {
            Controls.OnInputStart += OnInputStart;
            Controls.OnInputEnd += OnInputEnd;
        }

        public void Update() {
            if (_Drawing) {
                Drawing();
            }
        }

        private void OnInputStart() {
            _Drawing = true;
        }

        private void OnInputEnd() {
            _Drawing = false;
        }

        private void Drawing() {
            Debug.LogFormat("drawing: {0}", Controls.Position);
        }
    }
}
