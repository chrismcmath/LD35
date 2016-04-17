using UnityEngine;
using System;
using System.Collections;

using Rf.Controllers;
using Rf.Core;
using Rf.Models;

namespace Rf.Controls {
    public class DrawControls : MonoBehaviour {
        public static Action OnStartDrawing;
        public static Action OnStopDrawing;

        public Bounds DrawBounds;

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
            if (!GameController.CanDraw) return;

            if (OnStartDrawing != null) {
                OnStartDrawing();
            }

            _Drawing = true;
            _LineModel.ClearPoints();
        }

        private void OnInputEnd() {
            if (!_Drawing) return;

            if (OnStopDrawing != null) {
                OnStopDrawing();
            }
            _Drawing = false;
            _LineModel.Finalise();
        }

        private void Drawing() {
            Vector2 min = Camera.main.WorldToScreenPoint(DrawBounds.min);
            Vector2 max = Camera.main.WorldToScreenPoint(DrawBounds.max);
            Vector2 clampedPoint =
                new Vector2(
                        Mathf.Clamp(Controls.ScreenPosition.x,
                            min.x, max.x),
                        Mathf.Clamp(Controls.ScreenPosition.y,
                            min.y, max.y)
                        );
            _LineModel.AddPoint(clampedPoint);
        }
    }
}
