using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using Rf.Core;
using Rf.Models;

namespace Rf.Core {
    public class ConfigInit : MonoBehaviour {
        public Vector2 BackSpeed = Vector2.zero;

        public void Awake() {
            Config.BackSpeed = BackSpeed;
        }
    }
}
