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

    private bool isRunning;

    [Inject]
    public AutoMergeBoosterService(CubePool cubePool, MergeService mergeService)
    {
        this.cubePool = cubePool;
        this.mergeService = mergeService;
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

        await ExecuteBoosterSequence(a, b);

        isRunning = false;
    }

    private async UniTask ExecuteBoosterSequence(Cube a, Cube b)
    {
        a.DisablePhysics();
        b.DisablePhysics();

        Vector3 upOffset = Vector3.up * 5f;

        await UniTask.WhenAll(
            MergeAnimation.MoveCubeToPointAsync(a, a.transform.position + upOffset, 0.4f),
            MergeAnimation.MoveCubeToPointAsync(b, b.transform.position + upOffset, 0.4f)
        );

        await UniTask.Delay(150);

        await mergeService.ForceMergeAsync(a, b);
    }
}
