using UnityEngine;
using System;
using System.Collections;

namespace Rf.Controls {
    public class Controls : MonoBehaviour {
        public static Action OnInputStart;
        public static Action OnInputEnd;

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
            if (Input.GetMouseButtonDown(0) && OnInputStart != null) {
                OnInputStart();
            }

            if (Input.GetMouseButtonUp(0) && OnInputEnd != null) {
                OnInputEnd();
            }
        }
    }
}
