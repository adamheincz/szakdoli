using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Discovery;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayMenu : MonoBehaviour
{
    [SerializeField]
    private NetworkRoomManagerOwn networkManager = null;
    [SerializeField]
    private NetworkDiscovery networkDiscovery = null;
    [SerializeField]
    private GameObject playPanel = null;
    [SerializeField]
    private GameObject namePanel = null;
    [SerializeField]
    private GameObject serverListPanel = null;
    [SerializeField]
    private GameObject serverButtonPrefab = null;

    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        playPanel.SetActive(false);

        if (Application.platform == RuntimePlatform.Android)
        {
            namePanel.SetActive(true);
        } else
        {
            networkManager.StartServer();
            networkDiscovery.AdvertiseServer();
        }
    }

    public void StartGame()
    {
        networkManager.StartServer();
        networkDiscovery.AdvertiseServer();
    }

    public void StartLobby()
    {
        discoveredServers.Clear();
        //networkManager.StartServer();

        networkDiscovery.AdvertiseServer();
        Destroy(gameObject);
    }

    public void ShowAvailableServers()
    {
        namePanel.SetActive(false);
        serverListPanel.SetActive(true);

        discoveredServers.Clear();

        networkDiscovery.StartDiscovery();

    }

    public void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        networkManager.StartClient(info.uri);
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        GameObject newButton = Instantiate(serverButtonPrefab, serverListPanel.transform);

        Debug.Log("button text: " + newButton.GetComponentInChildren<TextMeshProUGUI>().text);

        newButton.GetComponentInChildren<TextMeshProUGUI>().text = info.EndPoint.Address.ToString();

        Debug.Log("info " + info.EndPoint.Address.ToString());

        newButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            newButton.GetComponent<Button>().interactable = false;

            Destroy(newButton);

            serverListPanel.SetActive(false);

            networkManager.networkAddress = info.EndPoint.Address.ToString();

            networkManager.StartClient();

        });


        Debug.Log("info: " + info);
    }


}
