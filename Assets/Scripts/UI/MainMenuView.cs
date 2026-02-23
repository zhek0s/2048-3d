using Assets.Scripts.Core.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
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
}