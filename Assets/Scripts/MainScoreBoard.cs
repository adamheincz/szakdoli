using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainScoreBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] playerNames;
    [SerializeField]
    private TextMeshProUGUI[] playerScores;
    [SerializeField]
    private NetworkRoomManagerOwn networkManager;

    private void Start()
    {
        networkManager = FindObjectOfType<NetworkRoomManagerOwn>();
    }

    public void SetPlayerInfos(List<MobilePlayer> mobilePlayers)
    {
        for (int i = 0; i < mobilePlayers.Count; i++)
        {
            playerNames[i].text = mobilePlayers[i].mobilePlayerName;
            playerScores[i].text = mobilePlayers[i].gameObject.GetComponent<MainScore>().GetScore().ToString();
        }
    }

    public void ReturnToMainMenu()
    {
        networkManager.StopServer();
        NetworkRoomManagerOwn.ResetNetworkManager();
    }


}
