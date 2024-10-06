using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BAM.C
{
    public class GameManager : MonoBehaviour
    {
        // Public static field convention name
        public static GameManager S_Instance;
        public BannerController bannerPrefab;
        public PlanetController planetPrefab;
        private AudioSource audioSource;
        public TMP_Text scoreText;

        public int score = 0;
        public enum GameState
        {
            Intro,
            Menu,
            Settings,
            Playing,
            Pause,
            GameOver,
            Exit
        }
        public GameState currentGameState = GameState.Intro;
        private GameStateManager _gsm;
        void Awake()
        {
            // Singleton pattern
            S_Instance = this;
        }        
        void Start()
        {
            _gsm = gameObject.GetComponent<GameStateManager>();
            audioSource = GetComponent<AudioSource>();
        }

        // Switch state to GamePlayState
        public void SwitchState(GameState newState)
        {
            switch(newState)
            {
                case GameState.Intro:
                    _gsm.SwitchState(_gsm.gameIntroState);
                    break;
                case GameState.Menu:
                    _gsm.SwitchState(_gsm.gamePlayState);
                    break;
                case GameState.Settings:
                    // _gsm.SwitchState(_gsm.gameSettingsState);
                    break;
                case GameState.Playing:
                    _gsm.SwitchState(_gsm.gamePlayState);
                    break;
                case GameState.Pause:
                    Debug.Log("Switching to Pause");
                    // _gsm.SwitchState(_gsm.gamePauseState);
                    break;
                case GameState.GameOver:
                    // _gsm.SwitchState(_gsm.gameOverState);
                    break;
                case GameState.Exit:
                    Debug.Log("Switching to Exit");
                    _gsm.SwitchState(_gsm.gameExitState);
                    break;
                default:
                    break;
            }
            currentGameState = newState;
        }
        public void AddScore(int points)
        {
            score += points;
            scoreText.text = "Score: " + score.ToString();
            // Update text mesh pro text
        }
        public void PlayAudio()
        {
            audioSource.Play();
        }
    }
}
