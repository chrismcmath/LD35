using UnityEngine;
using System;
using System.Collections;

using Vectrosity;

using Rf.Controls;
using Rf.Core;
using Rf.Models;

namespace Rf.View {
    public class LineView : MonoBehaviour {
        public int LineWidth = 30;
        public Color LineColor = Color.black;

        private Vector2[] _Points;
        private Material  _Material;
        private LineModel _LineModel;
        private VectorLine _LineRenderer;

        public void Start() {
            _LineModel = Global.Instance.LineModel;

            _Material = new Material(Shader.Find("Diffuse"));
            _Points = new Vector2[16000];

            LineModel.OnLineFinalized += OnLineFinalized;
            Rf.Controls.Controls.OnInputStart += OnInputStart;
        }

        public void OnDestroy() {
            VectorLine.Destroy(ref _LineRenderer);
        }

        private void OnInputStart() {
            ResetLine();
        }

        private void OnLineFinalized() {
            ResetLine();
        }

        public void LateUpdate() {
            Vector2[] points = _LineModel.GetPoints();
            if (_LineRenderer != null) {
                _LineRenderer.maxDrawIndex = Mathf.Max(0, points.Length - 1);
            }

            for (int i = 0; i < points.Length; i++) {
                _Points[i] = points[i];
            }

            if (points.Length > 2) {
                _LineRenderer.Draw();
            }
        }

        private void ResetLine() {
            if (_LineRenderer != null) {
                VectorLine.Destroy(ref _LineRenderer);
            }

            _LineRenderer = new VectorLine("Line", _Points, _Material, 3.0f, LineType.Continuous, Joins.Weld); 
        }
    }
}
