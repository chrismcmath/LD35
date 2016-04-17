using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

using Rf.Controls;
using Rf.Controllers;
using Rf.Models;

namespace Rf.Core {
    public class Global : MonoSingleton<Global> {
        public delegate void NewGameDelegate(string level);
        public static NewGameDelegate OnNewGame;

        public int CurrentLevel = 0;

        public LineModel LineModel;
        private GameController _Game;

        private bool _Started = false;

        public void Awake() {
            LineModel = GetComponentInChildren<LineModel>();
            _Game = GetComponent<GameController>();
        }

        public void Start() {
            if (SceneManager.GetActiveScene().name == "global") {
                GameController.CanDraw = false;
            }
        }

        public void Update() {
            if (!_Started && Input.GetMouseButtonDown(0)) {
                _Started = true;
                GoToLevel(CurrentLevel.ToString());
            }
        }

        public void GoToLevel(string level) {
            SceneManager.LoadScene(level);
            if (OnNewGame != null) {
                OnNewGame(level);
            }
        }

        public void NextLevel() {
            CurrentLevel++;
            GoToLevel(CurrentLevel.ToString());
        }

        public void ReloadLevel() {
            GoToLevel(SceneManager.GetActiveScene().name);
        }
    }
}
