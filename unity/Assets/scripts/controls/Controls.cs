using UnityEngine;
using System;
using System.Collections;

namespace Rf.Controls {
    public class Controls : MonoBehaviour {
        public static Action OnInputStart;
        public static Action OnInputEnd;

        //NOTE: apparently the fastest method
        //(http://gamedev.stackexchange.com/questions/114121/most-efficient-way-to-convert-vector3-to-vector2)
        public static Vector2 Position {
            get {
                Vector2 v2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                return v2;
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
