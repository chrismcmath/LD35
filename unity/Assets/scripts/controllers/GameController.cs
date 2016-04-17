using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Rf.Core;
using Rf.Models;

namespace Rf.Controllers {
    public class GameController : MonoBehaviour {
        public static bool CanDraw = true;

        public static Action OnWin;
        public static Action OnLose;

        private bool _AttemptInProgress = false;
        private List<BallController> _Balls = new List<BallController>();

        public void Awake() {
            LineModel.OnLineFinalized += OnLineFinalized;
            BallController.OnBallStrike += OnBallStrike;
            Rf.Controls.Controls.OnDoubleClick += OnDoubleClick;
        }

        public void Update() {
            CheckWinCondition();
            CheckLoseCondition();
        }

        private void OnDoubleClick() {
            Lose();
        }

        public void OnLineFinalized() {
            CanDraw = false;
        }

        public void OnBallStrike() {
            _AttemptInProgress = true;
        }

        private void OnLevelWasLoaded() {
            Debug.LogFormat("OnLevelWasLoaded");
            PrepScene();
            CanDraw = true;
        }

        private void CheckLoseCondition() {
            if (_AttemptInProgress && AllBallsStopped() && !AllBallsPrePotted()) {
                Lose();
            }
        }

        private void Lose() {
            _AttemptInProgress = false;
            if (OnLose != null) {
                OnLose();
            }
            StartCoroutine(ReloadAfterWait());
        }

        private void CheckWinCondition() {
            if (_AttemptInProgress && AllBallsPotted()) {
                _AttemptInProgress = false;
                if (OnWin != null) {
                    OnWin();
                }
                StartCoroutine(NextLevelAfterWait());
            }
        }

        private IEnumerator NextLevelAfterWait() {
            yield return new WaitForSeconds(1f);
            Global.Instance.NextLevel();
        }

        private IEnumerator ReloadAfterWait() {
            yield return new WaitForSeconds(1f);
            Global.Instance.ReloadLevel();
        }

        private void PrepScene() {
            _Balls = new List<BallController>(FindObjectsOfType<BallController>());
            Debug.LogFormat("PrepScene, balls: {0}", _Balls.Count);
        }

        private bool AllBallsPotted() {
            foreach (BallController ball in _Balls) {
                if (!ball.Potted) {
                    return false;
                }
            }
            return true;
        }

        private bool AllBallsPrePotted() {
            foreach (BallController ball in _Balls) {
                if (!ball.PrePotted) {
                    return false;
                }
            }
            return true;
        }

        private bool AllBallsStopped() {
            foreach (BallController ball in _Balls) {
                if (!ball.Stopped) {
                    return false;
                }
            }
            return true;
        }
    }
}
