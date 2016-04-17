using UnityEngine;
using System;
using System.Collections;

namespace Rf.Controls {
    public class Controls : MonoBehaviour {
        public static Action OnInputStart;
        public static Action OnInputEnd;
        public static Action OnDoubleClick;

        public const float DOUBLE_CLICK_SENSITIVITY = 0.2f;

        private bool _FirstClick = true;
        private float _LastClick = 0f;

        public static Vector3 ScreenPosition {
            get {
                return Input.mousePosition;
            }
        }

        public static Vector3 WorldPosition {
            get {
                return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public void Update() {
            if (!_FirstClick) {
                if (Time.time > _LastClick + DOUBLE_CLICK_SENSITIVITY) {
                    _FirstClick = true;
                }
            }

            if (Input.GetMouseButtonDown(0)) {
                if (!_FirstClick && OnDoubleClick != null) {
                    OnDoubleClick();
                    _FirstClick = true;
                } else {
                    _LastClick = Time.time;
                    _FirstClick = false;
                }
            }

            if (Input.GetMouseButtonDown(0) && OnInputStart != null) {
                OnInputStart();
            }

            if (Input.GetMouseButtonUp(0) && OnInputEnd != null) {
                OnInputEnd();
            }
        }
    }
}
