using UnityEngine;
using System;
using System.Collections;

using Rf.Core;
using Rf.Models;

namespace Rf.Controls {
    public class DrawControls : MonoBehaviour {
        private bool _Drawing = false;
        private LineModel _LineModel;

        public void Awake() {
            _LineModel = Global.Instance.LineModel;

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
            _LineModel.ClearPoints();
        }

        private void OnInputEnd() {
            _Drawing = false;
            _LineModel.Finalise();
        }

        private void Drawing() {
            _LineModel.AddPoint(Controls.ScreenPosition);
        }
    }
}
