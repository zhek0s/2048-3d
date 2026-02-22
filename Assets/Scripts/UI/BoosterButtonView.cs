using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BoosterButtonView : MonoBehaviour
{
    [SerializeField] private Button button;

    [Inject] private AutoMergeBoosterService boosterService;

    private bool isRunning;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
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
}
