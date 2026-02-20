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

        if (Input.GetMouseButtonUp(1)) //just for testing, remove later
        {
            SpawnNewCube();
        }
    }

    private void SpawnNewCube()
    {
        currentCube = spawner.Spawn();
        inputController.SetCube(currentCube);
    }
}
