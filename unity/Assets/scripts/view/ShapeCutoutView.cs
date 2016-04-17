using UnityEngine;
using System;
using System.Collections;

using Vectrosity;

using Rf.Controls;
using Rf.Core;
using Rf.Models;

namespace Rf.View {
    public class ShapeCutoutView : MonoBehaviour {
        private MeshFilter _MeshFilter;
        private Animator _Animator;

        public void Awake() {
            _Animator = GetComponent<Animator>();
            _MeshFilter = GetComponent<MeshFilter>();
        }

        public void Cutout(Mesh mesh) {
            _MeshFilter.mesh = mesh;
            _Animator.Play("cutout");
            StartCoroutine(DestroyAfterWait());
        }

        IEnumerator DestroyAfterWait() {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}
