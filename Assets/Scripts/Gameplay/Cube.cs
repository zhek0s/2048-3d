using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int Value { get; private set; }
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(int value)
    {
        this.Value = value;
        UpdateVisual();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
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
