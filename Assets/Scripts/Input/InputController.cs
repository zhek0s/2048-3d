using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float xLimit = 2f;
    private Cube currentCube;

    public void SetCube(Cube cube)
    {
        currentCube = cube;
    }

    void Update()
    {
        if (currentCube == null) return;

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x;
            float t = Mathf.InverseLerp(0,Screen.width, mouseX);
            float needX = Mathf.Lerp(-xLimit, xLimit, t);
            Vector3 pos = currentCube.transform.position;
            float changeX = needX - pos.x;
            pos.x = Mathf.Clamp(pos.x+ changeX, -xLimit, xLimit);
            currentCube.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentCube.Launch(launchForce);
            currentCube = null;
        }
    }
}
