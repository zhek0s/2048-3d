using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private float launchForce = 10f;
    private Cube currentCube;

    public void SetCube(Cube cube)
    {
        currentCube = cube;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && currentCube != null)
        {
            currentCube.Launch(launchForce);
            currentCube = null;
        }

    }
}
