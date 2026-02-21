using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Transform spawnPoint;
    public bool IsSpawnStopped = true;

    public Cube Spawn()
    {
        Cube cube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);

        int value = Random.value < 0.75f ? 2 : 4;
        cube.Init(value);

        return cube;
    }

    public async UniTask<Cube?> SpawnAsync()
    {
        await UniTask.Delay(400);
        if (IsSpawnStopped) return null;
        return Spawn();
    }
}
