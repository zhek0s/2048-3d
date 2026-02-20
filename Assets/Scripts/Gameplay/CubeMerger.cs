using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMerger : MonoBehaviour
{
    [SerializeField] private float mergeImpulseThreshold = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Cube other = collision.gameObject.GetComponent<Cube>();
        Cube current = GetComponent<Cube>();

        if (other == null) return;
        if (other == current) return;
        if (other.Value != current.Value) return;

        if (collision.impulse.magnitude < mergeImpulseThreshold)
            return;

        Merge(current, other);
    }
    private void Merge(Cube a, Cube b)
    {
        Destroy(a.gameObject);
        b.Init(a.Value * 2);
        FindObjectOfType<ScoreManager>().AddScore(a.Value/2);
    }
}
