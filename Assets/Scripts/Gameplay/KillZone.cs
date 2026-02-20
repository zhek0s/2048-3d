using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class KillZone : MonoBehaviour
{
    [Inject] private GameOverService gameOverService;

    private void OnTriggerEnter(Collider other)
    {
        if (gameOverService.IsGameOver) return;

        Cube cube = other.GetComponent<Cube>();
        if (cube == null) return;

        if (!cube.IsLaunched) return;

        gameOverService.TriggerGameOver();
    }
}
