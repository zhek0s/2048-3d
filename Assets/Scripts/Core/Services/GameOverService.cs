using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Services
{
    public class GameOverService
    {
        [Inject] private ScoreManager scoreManager;
        private readonly GameStateService gameStateService;
        private int lastScore = 0;

        public bool IsGameOver => gameStateService.CurrentState == GameState.GameOver;

        [Inject]
        public GameOverService(GameStateService gameStateService)
        {
            this.gameStateService = gameStateService;
        }

        public void TriggerGameOver()
        {
            lastScore = scoreManager.Score;
            scoreManager.SetScore(0);

            if (IsGameOver) return;

            gameStateService.SetState(GameState.GameOver);
        }

        public int GetLastScore()
        {
            return lastScore;
        }
    }
}