using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    GameOver
}

public class GameStateService
{
    public GameState CurrentState { get; private set; } = GameState.Menu;

    public event Action<GameState> OnStateChanged;

    public void SetState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
    }
}
