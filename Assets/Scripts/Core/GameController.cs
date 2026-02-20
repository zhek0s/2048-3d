using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject] private CubeSpawner spawner;
    [Inject] private InputController inputController;
    [Inject] private MergeService mergeService;
    [Inject] private GameStateService gameStateService;
    [Inject] private GameOverService gameOverService;

    private Cube currentCube;

    void Start()
    {
        gameStateService.OnStateChanged += HandleStateChanged;
        inputController.OnCubeLaunched += HandleOnCubeLaunched;
    }

    private void SpawnNewCube()
    {
        currentCube = spawner.Spawn();
        inputController.SetCube(currentCube);

        currentCube.OnCollisionEntered += HandleCollision;
    }

    private async void HandleCollision(Cube cube, Collision collision)
    {
        await mergeService.TryMergeAsync(cube, collision);
    }

    private void HandleStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                StartGame();
                break;

            case GameState.GameOver:
                gameOverService.TriggerGameOver();
                StopGame();
                break;
        }
    }

    private async void HandleOnCubeLaunched()
    {
        currentCube = await spawner.SpawnAsync();
        currentCube.OnCollisionEntered += HandleCollision;
        inputController.SetCube(currentCube);
    }

    private async void StartGame()
    {
        spawner.IsSpawnStopped = false;
        await UniTask.Delay(100);
        SpawnNewCube();
    }

    private void StopGame()
    {
        spawner.IsSpawnStopped = true;
        inputController.SetCube(null);
    }

    public void RestartGame()
    {

        foreach (var cube in FindObjectsOfType<Cube>())
        {
            Destroy(cube.gameObject);
        }

        gameStateService.SetState(GameState.Playing);
    }
}
