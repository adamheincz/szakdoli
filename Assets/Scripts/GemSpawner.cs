using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GemSpawner : MonoBehaviour
{
    [SerializeField]
    private Tilemap ground;
    [SerializeField]
    private GameObject bigGemPrefab;

    private BoundsInt bound;
    private float timer = 0f;

    private void Start()
    {
        bound = ground.cellBounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < bigGemPrefab.GetComponent<Gem>().spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnGem();
            timer = 0;
        }
    }

    public void SpawnGem()
    {
        Vector3 randomPos;

        do
        {
            randomPos = ground.GetCellCenterWorld(new Vector3Int(Random.Range(bound.xMin, bound.xMax), Random.Range(bound.yMin, bound.yMax), 0));
        } while (!ground.HasTile(ground.WorldToCell(randomPos)));

        GameObject bigGem = Instantiate(bigGemPrefab, randomPos, Quaternion.identity);

        bigGem.transform.SetParent(transform);

    }
}
