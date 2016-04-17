using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using Rf.Controls;
using Rf.Core;
using Rf.Models;

namespace Rf.Controllers {
    public class DrawZoneController : MonoBehaviour {
        private Animator _Animator;
        private CanvasGroup _InputBox;
        private bool _StartedDrawing = false;

        public float Alpha = 0f;

        public void Awake() {
            Global.OnNewGame += OnNewGame;
            DrawControls.OnStartDrawing += OnStartDrawing;
            DrawControls.OnStopDrawing += OnStopDrawing;

            _Animator = GetComponent<Animator>();
            _InputBox = GetComponent<CanvasGroup>();
        }

        public void OnNewGame(string level) {
            _StartedDrawing = false;
            StartCoroutine(ThrobAfterWait());
        }

        IEnumerator ThrobAfterWait() {
            yield return new WaitForSeconds(2f);
            if (!_StartedDrawing) {
                _Animator.Play("throb");
            }
        }

        public void OnStartDrawing() {
            _StartedDrawing = true;
            Debug.LogFormat("OnStartDrawing");
            _Animator.Play("half");
        }

        public void OnStopDrawing() {
            Debug.LogFormat("OnStopDrawing");
            _Animator.Play("hide");
        }

        public void Update() {
            _InputBox.alpha = Alpha;
        }
    }
}
