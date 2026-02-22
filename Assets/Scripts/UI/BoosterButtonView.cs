using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BoosterButtonView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject mergeButton;

    [Inject] private AutoMergeBoosterService boosterService;
    [Inject] private GameStateService gameStateService;

    private bool isRunning;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
        gameStateService.OnStateChanged += HandleStateChanged;
    }

    private async void OnClick()
    {
        if (isRunning) return;

        isRunning = true;
        button.interactable = false;

        await boosterService.RunAsync();

        button.interactable = true;
        isRunning = false;
    }

    private void HandleStateChanged(GameState state)
    {
        if (state == GameState.Playing)
            mergeButton.gameObject.SetActive(true);
        else
            mergeButton.gameObject.SetActive(false);
    }
}
