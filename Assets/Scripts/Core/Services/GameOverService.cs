using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverService
{
    public bool IsGameOver { get; private set; }

    public void TriggerGameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;

        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
    }
}
