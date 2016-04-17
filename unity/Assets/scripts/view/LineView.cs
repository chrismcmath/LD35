using UnityEngine;
using System;
using System.Collections;

using Vectrosity;

using Rf.Controllers;
using Rf.Controls;
using Rf.Core;
using Rf.Models;

namespace Rf.View {
    public class LineView : MonoBehaviour {
        public int LineWidth = 5;
        public Color LineColor = Color.black;
        public Material Mat;

        private Vector2[] _Points;
        private LineModel _LineModel;
        private VectorLine _LineRenderer;

        public void Start() {
            _LineModel = Global.Instance.LineModel;

            _Points = new Vector2[16000];

            LineModel.OnLineFinalized += OnLineFinalized;
            Rf.Controls.Controls.OnInputStart += OnInputStart;
        }

        public void OnDestroy() {
            VectorLine.Destroy(ref _LineRenderer);
        }

        private void OnInputStart() {
            if (GameController.CanDraw) {
                ResetLine();
            }
        }

        private void OnLineFinalized() {
            Debug.LogFormat("destroy line");
            VectorLine.Destroy(ref _LineRenderer);
        }

        public void LateUpdate() {
            Vector2[] points = _LineModel.GetPoints();
            if (_LineRenderer != null) {
                _LineRenderer.maxDrawIndex = Mathf.Max(0, points.Length - 1);
            } else {
                return;
            }

            for (int i = 0; i < points.Length; i++) {
                _Points[i] = points[i];
            }

            if (points.Length > 2) {
                _LineRenderer.Draw();
            }
        }

        private void ResetLine() {
            VectorLine.Destroy(ref _LineRenderer);

            _LineRenderer = new VectorLine("Line", _Points, Mat, LineWidth, LineType.Continuous, Joins.Fill); 
        }
    }
}
