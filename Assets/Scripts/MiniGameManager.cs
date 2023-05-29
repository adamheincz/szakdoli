using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreBoardPrefab;

    public List<PlayerController> players = new List<PlayerController>();
    public bool gameStarted = false;
    public bool isOver = false;

    public UnityEvent OnMiniGameOver;

    private void Update()
    {
        if(EndCondition() && !isOver && gameStarted)
        {
            isOver = true;
            gameStarted = false;
            MiniGameOver();
        }
    }

    public virtual void MiniGameOver()
    {
        SortPlayersByScore();
        AddPlayerScoreToMainScore();
        InstantiateScoreBoard();
        OnMiniGameOver?.Invoke();
    }

    public virtual bool EndCondition()
    {
        return false;
    }

    public void AddPlayer(PlayerController player)
    {
        players.Add(player);
    }

    public void SortPlayersByScore()
    {
        players = players.OrderByDescending(p => p.GetComponentInParent<Score>().GetScore()).ToList();
    }

    public void AddPlayerScoreToMainScore()
    {
        foreach (PlayerController player in players)
        {
            player.mobilePlayer.GetComponent<MainScore>().AddScore(player.GetComponent<Score>().GetScore());
        }
    }

    public virtual void StartMiniGame()
    {
        gameStarted = true;
    }

    public virtual void InstantiateScoreBoard()
    {
        GameObject scoreBoardObject = Instantiate(scoreBoardPrefab);

        ScoreBoard scoreBoard = scoreBoardObject.GetComponent<ScoreBoard>();
         
        List<string> playerNames = new List<string>();
        List<float> playerScores = new List<float>();
        List<Sprite> playerSprites = new List<Sprite>();

        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("player" + i + " name: " + players[i].GetPlayerName());
            playerNames.Add(players[i].GetPlayerName());
            playerScores.Add(players[i].GetComponentInParent<Score>().GetScore());
            playerSprites.Add(players[i].GetComponentInParent<SpriteRenderer>().sprite);
        }

        scoreBoard.SetPlayerInfos(playerNames, playerSprites, playerScores);
    }
}
