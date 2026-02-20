using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Transform spawnPoint;
    public Cube Spawn()
    {
        Cube cube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);

        int value = Random.value < 0.75f ? 2 : 4;
        cube.Init(value);

        return cube;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
