using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScore : MonoBehaviour
{
    private float mainScore = 0;

    public void AddScore(float score)
    {
        mainScore += score;
    }

    public float GetScore()
    {
        return mainScore;
    }
}
