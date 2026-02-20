using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button startButton;

    [Inject] private GameStateService gameStateService;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        gameStateService.SetState(GameState.Playing);
        startButton.gameObject.SetActive(false);
    }
}
