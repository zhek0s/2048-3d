using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private int initialSize = 20;

    private Queue<Cube> pool = new Queue<Cube>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewCube();
        }
    }

    private Cube CreateNewCube()
    {
        Cube cube = Instantiate(cubePrefab, transform);
        cube.gameObject.SetActive(false);
        pool.Enqueue(cube);
        return cube;
    }

    public Cube Get()
    {
        if (pool.Count == 0)
        {
            CreateNewCube();
        }

        Cube cube = pool.Dequeue();
        return cube;
    }

    public void Return(Cube cube)
    {
        cube.Deactivate();
        pool.Enqueue(cube);
    }
}
