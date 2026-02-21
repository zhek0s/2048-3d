using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public bool IsSpawnStopped = true;

    [Inject] private CubePool cubePool;

    public Cube Spawn()
    {
        int value = Random.value < 0.75f ? 2 : 4;
        return SpawnAt(spawnPoint.position, value);
    }

    public async UniTask<Cube?> SpawnAsync()
    {
        await UniTask.Delay(400);
        if (IsSpawnStopped) return null;
        return Spawn();
    }

    public Cube SpawnAt(Vector3 position, int value)
    {
        Cube cube = cubePool.Get();
        cube.Init(value, position);
        return cube;
    }
}
