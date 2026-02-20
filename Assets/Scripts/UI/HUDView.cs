using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using Unity.VisualScripting;

public class HUDView : MonoBehaviour
{
    [SerializeField] private GameObject scoreText;
    [Inject] private GameStateService gameStateService;
    void Start()
    {
        gameStateService.OnStateChanged += HandleStateChanged;
    }

    private void HandleStateChanged(GameState state)
    {
        if (state == GameState.Playing)
            scoreText.gameObject.SetActive(true);
        else
            scoreText.gameObject.SetActive(false);
    }
}
