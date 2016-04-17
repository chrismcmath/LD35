using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Rf.Utils;

namespace Rf.Models {
    public class LineModel : MonoBehaviour {
        public static Action OnLineFinalized;

        public float AngleThreshold = 10f;
        public float DistanceThreshold = 5f;

        private int _Index;
        private List<Vector2> _Points;

        public void Start() {
            _Points = new List<Vector2>();
        }

        public void AddPoint(Vector2 point) {
            _Points.Add(point);
        }

        public void ClearPoints() {
            _Points.Clear();
        }

        public void Finalise() {
            _Index = -1;
            do {
                _Index = CheckList();
                if (_Index != -1) {
                    _Points.RemoveAt(_Index);
                }
            } while (_Index != -1);

            if (OnLineFinalized != null) {
                OnLineFinalized();
            }
        }

        private int CheckList() {
            for (int i = 1; i < _Points.Count - 1; i++) {
                float radians;

                float distance = (_Points[i] - _Points[i-1]).magnitude;
                if (distance < DistanceThreshold) {
                    return i;
                }

                if (i == _Points.Count - 2) {
                    radians = Vector2.Angle(
                            _Points[i+1] - _Points[i],
                            _Points[0] - _Points[i]);
                } else {
                    radians = Vector2.Angle(
                            _Points[i+1] - _Points[i],
                            _Points[i+2] - _Points[i]);
                }

                float degrees = UnityEngine.Mathf.Rad2Deg * radians;
                while (degrees < 0f) {
                    degrees += 360f;
                }
                
                while (degrees >= 360) {
                    degrees -= 360f;
                }

                if (degrees >= 180f) {
                    degrees = 360f - degrees;
                }

                if (degrees < AngleThreshold) {
                    return i+1;
                }
            }
            return -1;
        }

        public Vector2[] GetPoints() {
            return _Points.ToArray();
        }

        public Vector2[] GetWorldPoints() {
            Vector2[] worldPoints = new Vector2[_Points.Count];
            for (int i = 0; i < _Points.Count; i++) {
                worldPoints[i] = Camera.main.ScreenToWorldPoint(_Points[i]);
            }
            return worldPoints;
        }
    }
}
