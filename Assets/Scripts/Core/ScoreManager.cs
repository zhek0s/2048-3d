using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int _score;
    public int Score { 
        get { return _score; }
        private set
        {
            _score = value;
            GetComponent<TextMeshPro>().text = $"Score: {_score}";
        } 
    }

    void Start()
    {
        Score = 0;
    }

    public void AddScore(int value)
    {
        Score += value;
    }
}
