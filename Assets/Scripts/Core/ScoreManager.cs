using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }

    void Start()
    {
        Score = 0;
    }

    void Update()
    {
        
    }

    public void AddScore(int value)
    {
        Score += value;
        Debug.Log("Score: " + Score);
    }
}
