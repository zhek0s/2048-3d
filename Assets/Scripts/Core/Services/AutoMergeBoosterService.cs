using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AutoMergeBoosterService
{
    private readonly CubePool cubePool;
    private readonly MergeService mergeService;
    private readonly ParticlePool particlePool;

    private bool isRunning;

    [Inject]
    public AutoMergeBoosterService(
        CubePool cubePool, 
        MergeService mergeService,
        ParticlePool particlePool) 
    {
        this.cubePool = cubePool;
        this.mergeService = mergeService;
        this.particlePool = particlePool;
    }

    public async UniTask RunAsync()
    {
        if (isRunning) return;
        isRunning = true;

        var cubes = Object.FindObjectsOfType<Cube>().Where(c => c.IsLaunched).ToList();
        var pair = cubes.GroupBy(c => c.Value).FirstOrDefault(g => g.Count() >= 2);

        if (pair == null)
        {
            isRunning = false;
            return;
        }

        Cube a = pair.ElementAt(0);
        Cube b = pair.ElementAt(1);

        await MergeAnimation.BoosterSequence(a, b);
        Vector3 center = (a.transform.position + b.transform.position) / 2f;
        await UniTask.WhenAll(
            particlePool.PlayAsync(center),
            mergeService.ForceMergeAsync(a, b)
        );

        isRunning = false;
    }
}
