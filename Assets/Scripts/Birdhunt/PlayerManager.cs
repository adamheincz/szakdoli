using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int numberOfPlayers = 5;

    [SerializeField]
    private GameObject playerPrefab;

    private List<GameObject> players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        float startingY = 2.3f;
        float positionY = startingY;
        for(int i = 0; i < numberOfPlayers; i++)
        {
            Debug.Log(positionY);
            players.Add(Instantiate(playerPrefab));
            players[i].transform.position = new Vector3(3, positionY, players[i].transform.position.z);
            players[i].GetComponent<PlayerConrollerScript>().SetPlayerIndex(i);

            positionY -= (startingY*2)/5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(AllDead())
        {

        }
    }

    bool AllDead()
    {
        if(players.Count == 0)
        {
            return true;
        }
        return false;
    }
}
