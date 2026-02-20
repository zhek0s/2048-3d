using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
