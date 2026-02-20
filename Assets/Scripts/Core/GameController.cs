using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject] private CubeSpawner spawner;
    [Inject] private InputController inputController;
    [Inject] private ScoreManager scoreManager;
    [Inject] private MergeService mergeService;

    private Cube currentCube;

    void Start()
    {
        SpawnNewCube();
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
}
