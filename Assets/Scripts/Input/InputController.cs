using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InputController : MonoBehaviour
{
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float xLimit = 2f;
    private Cube? currentCube;
    private Rigidbody? currentRb;
    private float targetX;

    public event Action OnCubeLaunched;

    public void SetCube(Cube? cube)
    {
        currentCube = cube;
        if (cube != null)
        {
            currentRb = cube.gameObject.GetComponent<Rigidbody>();
            targetX = cube.transform.position.x;
        }
    }

    void Update()
    {
        if (currentCube == null) return;

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x;
            float t = Mathf.InverseLerp(0,Screen.width, mouseX);
            targetX = Mathf.Lerp(-xLimit, xLimit, t);
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentCube.Launch(launchForce);
            currentCube = null;
            currentRb = null;
            OnCubeLaunched.Invoke();
        }
    }

    void FixedUpdate()
    {
        if (currentRb == null) return;

        Vector3 pos = currentRb.position;
        pos.x = Mathf.Clamp(targetX, -xLimit, xLimit);

        currentRb.MovePosition(pos);
    }
}
