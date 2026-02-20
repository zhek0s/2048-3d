using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CubeSpawner spawner;
    [SerializeField] private InputController inputController;
    private Cube currentCube;
    void Start()
    {
        SpawnNewCube();
    }

    void Update()
    {

    }

    private void SpawnNewCube()
    {
        currentCube = spawner.Spawn();
        inputController.SetCube(currentCube);
    }
}
