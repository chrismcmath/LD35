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

        public AudioSource CuttingSfx;
        public AudioSource RipSfx;

        private float _Pitch;
        private Vector2[] _Points;
        private LineModel _LineModel;
        private VectorLine _LineRenderer;

        public void Start() {
            _LineModel = Global.Instance.LineModel;

            _Points = new Vector2[16000];

            LineModel.OnLineFinalized += OnLineFinalized;
            LineModel.OnPointAdded += OnPointAdded;
            Rf.Controls.Controls.OnInputStart += OnInputStart;
        }

        public void OnDestroy() {
            VectorLine.Destroy(ref _LineRenderer);
        }

        private void OnInputStart() {
            if (GameController.CanDraw) {
                CuttingSfx.Play();
                ResetLine();
            }
        }

        public void Update() {
            if (CuttingSfx.isPlaying) {
                _Pitch -= Time.deltaTime * 10f;
                CuttingSfx.pitch = Mathf.Max(_Pitch, 0f);
            }
        }

        private void OnPointAdded(Vector2 point) {
            float mag = 1f;
            if (_LineModel.GetPoints().Length > 1) {
                mag = Vector2.Distance(point, _LineModel.GetPoints()[_LineModel.GetPoints().Length - 2]);
            }
            _Pitch = Mathf.Max(Mathf.Min(mag * 0.5f, 2f), 0.5f);
        }

        private void OnLineFinalized() {
            CuttingSfx.Stop();
            RipSfx.Play();
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
