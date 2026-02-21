using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    public int Value { get; private set; }
    public bool IsLaunched { get; private set; }
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

    void OnTriggerExit(Collider other)
    {
        IsLaunched = true;
    }

    public void Init(int value)
    {
        this.Value = value;
        UpdateVisual();
    }

    public void Launch(float force)
    {
        IsLaunched = false;
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }

    public void Bounce(float upwardForce, Vector3 movingForce)
    {
        Vector3 randomDirection = new Vector3(Random.Range(-0.2f, 0.2f), 1f, Random.Range(-0.2f, 0.2f)).normalized;

        Vector3 force = randomDirection * upwardForce + movingForce;

        rb.AddForce(force, ForceMode.Impulse);
    }

    private void UpdateVisual()
    {
        var cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", GetColorFor(Value));

        TextMeshPro[] texts = GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro textMeshPro in texts)
            textMeshPro.text = Value.ToString();
    }

    private Color GetColorFor(int number)
    {
        return number switch
        {
            2 => Color.red,
            4 => Color.green,
            8 => Color.blue,
            16 => Color.white,
            32 => Color.yellow,
            64 => Color.cyan,
            128 => Color.magenta,
            _ => Color.gray,
        };
    }
}
