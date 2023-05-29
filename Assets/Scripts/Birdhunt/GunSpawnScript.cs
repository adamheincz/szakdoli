using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunSpawnScript : MonoBehaviour
{
    public GameObject gunPrefab;

    [SerializeField]
    private float yOffset = 2.5f;
    [SerializeField]
    private float spawnRate;
    [SerializeField]
    private int maxNumOfGuns = 8;

    private float timer = 0f;
    private int gunCounter = 1;

    public UnityEvent OnRoundOver;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = gunPrefab.GetComponent<GunControllerScript>().lifeTime;
        SpawnGun();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        } else
        {
            OnRoundOver?.Invoke();
            for(int i = 0; i <= gunCounter; i++)
            {
                SpawnGun();
            }
            if(gunCounter < maxNumOfGuns)
            {
                gunCounter++;
            }
            timer = 0;
        }
    }

    /// <summary>
    /// This method is responsible for spawning a gun at a random location between the highest and lowest points.
    /// </summary>
    void SpawnGun()
    {
        float lowestPosition = transform.position.y - yOffset;
        float highestPosition = transform.position.y + yOffset;

        GameObject gun = Instantiate(gunPrefab, new Vector3(transform.position.x, Random.Range(lowestPosition, highestPosition), 0), transform.rotation);

        gun.transform.SetParent(transform);
        
    }
}
