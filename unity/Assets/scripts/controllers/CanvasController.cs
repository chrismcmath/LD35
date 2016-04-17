using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using Rf.Core;
using Rf.Models;

namespace Rf.Controllers {
    public class CanvasController : MonoBehaviour {
        public Animator Win;
        public Animator Lose;
        public Animator Level;
        public AudioSource WinSfx;
        public AudioSource LoseSfx;
        public AudioSource NewLevelSfx;
        public ParticleSystem WinParticles;

        public void Awake() {
            Global.OnNewGame += OnNewGame;
            GameController.OnWin += OnWin;
            GameController.OnLose += OnLose;
        }

        public void OnDestroy() {
            Global.OnNewGame -= OnNewGame;
            GameController.OnWin -= OnWin;
            GameController.OnLose -= OnLose;
        }

        public void OnNewGame(string level) {
            Level.Play("InOut");
            NewLevelSfx.Play();
            foreach (Text t in Level.GetComponentsInChildren<Text>()) {
                t.text = level;
            }
        }

        public void OnWin() {
            Debug.LogFormat("OnWin");
            Win.Play("InOut");
            WinSfx.Play();
            WinParticles.Play();
            StartCoroutine(StopParticles(WinParticles));
        }

        IEnumerator StopParticles(ParticleSystem particles) {
            yield return new WaitForSeconds(0.5f);
            particles.Stop();
        }

        public void OnLose() {
            Debug.LogFormat("OnLose");
            Lose.Play("InOut");
            LoseSfx.Play();
        }
    }
}
