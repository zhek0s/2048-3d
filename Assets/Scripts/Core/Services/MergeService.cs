using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MergeService
{
    private readonly float mergeThreshold;
    private readonly CubeSpawner spawner;
    private readonly ScoreManager scoreManager;

    [Inject]
    public MergeService(
        [Inject(Id = "MergeThreshold")] float threshold,
        CubeSpawner spawner,
        ScoreManager scoreManager)
    {
        mergeThreshold = threshold;
        this.spawner = spawner;
        this.scoreManager = scoreManager;
    }

    public void TryMerge(Cube cube, Collision collision)
    {
        Cube other = collision.gameObject.GetComponent<Cube>();

        if (other == null) return;
        if (other == cube) return;
        if (other.Value != cube.Value) return;
        if (collision.impulse.magnitude < mergeThreshold) return;

        Merge(cube, other);
    }

    private void Merge(Cube a, Cube b)
    {
        Vector3 position = (a.transform.position + b.transform.position) / 2f;
        int newValue = a.Value * 2;

        Object.Destroy(b.gameObject);

        a.transform.position = position;
        a.Init(newValue);

        scoreManager.AddScore(newValue/4);
    }
}
