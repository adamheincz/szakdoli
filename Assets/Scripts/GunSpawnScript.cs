using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawnScript : MonoBehaviour
{
    public GameObject gun;

    [SerializeField]
    private float yOffset = 15;
    [SerializeField]
    private float spawnRate;
    [SerializeField]
    private int maxNumOfWaves = 8;

    private float timer = 0;
    private int waveCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = gun.GetComponent<GunControllerScript>().lifeTime;
        spawnGun();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        } else
        {
            for(int i = 0; i <= waveCounter; i++)
            {
                spawnGun();
            }
            if(waveCounter < maxNumOfWaves)
            {
                waveCounter++;
            }
            timer = 0;
        }
    }

    /// <summary>
    /// This method is responsible for spawning a gun at a random location between the highest and lowest points.
    /// </summary>
    void spawnGun()
    {
        float lowestPosition = transform.position.y - yOffset;
        float highestPosition = transform.position.y + yOffset;

        Instantiate(gun, new Vector3(transform.position.x, Random.Range(lowestPosition, highestPosition), 0), transform.rotation);
    }
}
