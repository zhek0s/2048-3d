using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using Assets.Scripts.Core.Services;
using Assets.Scripts.Core;

namespace Assets.Scripts.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject lastScoreText;

        [Inject] private GameStateService gameStateService;
        [Inject] private GameOverService gameOverService;
        [Inject] private GameController gameController;
        void Start()
        {
            gameStateService.OnStateChanged += HandleStateChanged;
            restartButton.onClick.AddListener(Restart);
        }

        private void HandleStateChanged(GameState state)
        {
            if (state == GameState.GameOver)
            {
                restartButton.gameObject.SetActive(true);
                lastScoreText.gameObject.SetActive(true);
                lastScoreText.gameObject.GetComponent<TMP_Text>().text = $"Your score: {gameOverService.GetLastScore()}";
            }
        }

        private void Restart()
        {
            restartButton.gameObject.SetActive(false);
            lastScoreText.gameObject.SetActive(false);
            gameController.RestartGame();
        }
    }
}