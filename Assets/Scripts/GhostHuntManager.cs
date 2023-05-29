using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHuntManager : MiniGameManager
{
    [SerializeField]
    private int scorePerRound = 25;

    public override bool EndCondition()
    {
        if (players.Count != 0)
        {
            int aliveCounter = 0;

            //Debug.Log("players_length: " + players);

            foreach (PlayerController player in players)
            {
                // Debug.Log("player_is_dead: " + player.gameObject.GetComponent<Health>().isDead);
                if (player.gameObject.GetComponent<Health>().isDead == false)
                {
                    aliveCounter++;
                }
            }

            if (aliveCounter == 0)
            {
                return true;
            }

            return false;
        }
        return false;
    }

    public override void MiniGameOver()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        base.MiniGameOver();
    }

    public void AddScoreForAlivePlayers()
    {
        foreach (PlayerController player in players)
        {
            if (player.gameObject.GetComponent<Health>().isDead == false)
            {
                player.gameObject.GetComponent<Score>().AddScore(scorePerRound);
            }
        }
    }
}
