using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System.Linq;
using System;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainScoreBoard;
    [SerializeField]
    private TextMeshProUGUI countDownText;
    [SerializeField]
    private int countDownSeconds;

    public List<NetworkStartPosition> startPositions = new List<NetworkStartPosition>();

    private int currentGameIndex = 0;

    public void StartGame()
    {
        Debug.Log("start_next_minigame");
        LoadNextMiniGame();
    }

    public void LoadNextMiniGame()
    {
        foreach ((GameObject, GameObject) miniGamePlayer in NetworkRoomManagerOwn.miniGamesPlayers)
        {
            miniGamePlayer.Item1.SetActive(false);
        }

        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach (PlayerController player in players)
        {
            if (player.gameObject != null)
            {
                Destroy(player.gameObject);
            }
        }

        if (currentGameIndex == NetworkRoomManagerOwn.miniGamesPlayers.Count)
        {
            Debug.Log("level_manager_no_more_games_left");
            GameOver();

        }
        else
        {
            GameObject miniGame =  NetworkRoomManagerOwn.miniGamesPlayers[currentGameIndex].Item1;
            miniGame.SetActive(true);

            startPositions = FindObjectsOfType<NetworkStartPosition>().ToList();

            StartCoroutine(CountDown(miniGame.GetComponentInChildren<MiniGameManager>()));
        }
    }

    public void GameOver()
    {
        List<MobilePlayer> mobilePlayers = NetworkRoomManagerOwn.mobilePlayers.OrderByDescending(m => m.gameObject.GetComponent<MainScore>().GetScore()).ToList();

        mainScoreBoard.SetActive(true);

        mainScoreBoard.GetComponent<MainScoreBoard>().SetPlayerInfos(mobilePlayers);
    }

    private IEnumerator CountDown(MiniGameManager miniGameManager)
    {
        while (countDownSeconds > 0)
        {
            countDownText.text = countDownSeconds.ToString();

            yield return new WaitForSeconds(1);

            countDownSeconds--;
        }

        countDownText.text = "GO!";

        int playerIndex = 0;

        foreach (MobilePlayer mobilePlayer in NetworkRoomManagerOwn.mobilePlayers)
        {
            mobilePlayer.RpcSpawnPlayer(currentGameIndex, startPositions[playerIndex].transform.position, startPositions[playerIndex].transform.rotation);
            Debug.Log("itt_lenne_a_mini_game_loading");
            playerIndex++;
        }

        miniGameManager.StartMiniGame();

        currentGameIndex++;

        yield return new WaitForSeconds(1);

        countDownText.text = "";
    }
}
