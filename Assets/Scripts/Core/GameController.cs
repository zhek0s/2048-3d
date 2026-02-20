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
    }

    void Update()
    {

        if (Input.GetMouseButtonUp(1)) //just for testing, remove later
        {
            SpawnNewCube();
        }
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
                gameOverService.TriggerGameOver(scoreManager.Score);
                StopGame();
                break;
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        SpawnNewCube();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        foreach (var cube in FindObjectsOfType<Cube>())
        {
            Destroy(cube.gameObject);
        }

        gameStateService.SetState(GameState.Playing);
    }
}
