using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

public class MergeService
{
    private readonly float mergeThreshold;
    private readonly float bounceForce;
    private readonly CubePool cubePool;
    private readonly ScoreManager scoreManager;

    private bool isMerging;

    [Inject]
    public MergeService(
        [Inject(Id = "MergeThreshold")] float threshold,
        [Inject(Id = "MergeBounceForce")] float bounceForce,
        CubePool cubePool,
        ScoreManager scoreManager)
    {
        mergeThreshold = threshold;
        this.bounceForce = bounceForce;
        this.cubePool = cubePool;
        this.scoreManager = scoreManager;
    }

    public async UniTask TryMergeAsync(Cube cube, Collision collision)
    {
        if (isMerging) return;
        Cube other = collision.gameObject.GetComponent<Cube>();

        if (other == null) return;
        if (other == cube) return;
        if (other.Value != cube.Value) return;
        if (collision.impulse.magnitude < mergeThreshold) return;

        isMerging = true;

        await MergeAsync(cube, other);

        isMerging = false;
    }

    private async UniTask MergeAsync(Cube a, Cube b)
    {
        Vector3 position = (a.transform.position + b.transform.position) / 2f;
        int newValue = a.Value * 2;
        Vector3 movingForce = a.GetComponent<Rigidbody>().velocity;

        a.GetComponent<Rigidbody>().velocity = Vector3.zero;
        b.GetComponent<Rigidbody>().velocity = Vector3.zero;

        await UniTask.Delay(100);

        cubePool.Return(b);

        a.transform.position = position;
        a.Init(newValue, position);
        a.Bounce(bounceForce, movingForce/bounceForce);

        scoreManager.AddScore(newValue/4);
    }
}
