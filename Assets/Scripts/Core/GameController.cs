using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CubeSpawner spawner;
    [SerializeField] private InputController inputController;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private float mergeThreshold = 2f;

    private MergeService mergeService;
    private Cube currentCube;

    void Start()
    {
        mergeService = new MergeService(
            mergeThreshold, 
            spawner,
            scoreManager
            );

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

    private void HandleCollision(Cube cube, Collision collision)
    {
        mergeService.TryMerge(cube, collision);
    }
}
