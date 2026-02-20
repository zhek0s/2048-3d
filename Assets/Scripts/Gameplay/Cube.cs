using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int Value { get; private set; }
    private Rigidbody rb;
    public event Action<Cube, Collision> OnCollisionEntered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEntered?.Invoke(this, collision);
    }

    public void Init(int value)
    {
        this.Value = value;
        UpdateVisual();
    }

    public void Launch(float force)
    {
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }

    private void UpdateVisual()
    {
        Debug.Log($"Cube spawned. Value: {Value}");
    }
}
