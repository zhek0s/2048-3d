using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameOverService
{
    private readonly GameStateService gameStateService;
    private int lastScore = 0;

    public bool IsGameOver => gameStateService.CurrentState == GameState.GameOver;

    [Inject]
    public GameOverService(GameStateService gameStateService)
    {
        this.gameStateService = gameStateService;
    }

    public void TriggerGameOver(int score = 0)
    {
        lastScore = score;

        if (IsGameOver) return;

        gameStateService.SetState(GameState.GameOver);
    }

    public int GetLastScore()
    {
        return lastScore;
    }
}
