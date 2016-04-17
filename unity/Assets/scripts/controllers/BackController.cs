using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using Rf.Core;
using Rf.Models;

namespace Rf.Controllers {
    public class BackController : MonoBehaviour {
        private MeshRenderer _Renderer;

        public void Awake() {
            _Renderer = GetComponent<MeshRenderer>();
        }

        public void Update() {
            Vector2 offset = Time.time * Config.BackSpeed * - 1f;
            _Renderer.material.SetTextureOffset("_MainTex", new Vector2(offset.x, offset.y));
        }
    }
}
