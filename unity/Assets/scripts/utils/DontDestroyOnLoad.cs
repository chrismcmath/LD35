using UnityEngine;
using System;
using System.Collections;

namespace Rf.Utils {
    public class DontDestroyOnLoad : MonoBehaviour {
        public void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}
