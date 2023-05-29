using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private SpawnPoint[] spawnPoints;

    private void Awake()
    {
        instance = this;
        spawnPoints = GetComponentsInChildren<SpawnPoint>();

    }
}
