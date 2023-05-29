using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    private float score = 0;

    public UnityEvent<string> OnAddScore;

    public void AddScore(float value)
    {
        score += value;
        OnAddScore?.Invoke(score.ToString());
    }

    public float GetScore()
    {
        return score;
    }


}
