using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MiniGameManager
{
    public override bool EndCondition()
    {
        if (players.Count == NetworkRoomManagerOwn.mobilePlayers.Count)
        {
            int aliveCounter = 0;

            //Debug.Log("players_length: " + players);

            foreach (PlayerController player in players)
            {
                //Debug.Log("player_is_dead: " + player.gameObject.GetComponent<Health>().isDead);
                if (player.gameObject.GetComponent<Health>().isDead == false)
                {
                    aliveCounter++;
                }
            }

            if (aliveCounter <= 1)
            {
                return true;
            }

            return false;
        }
        return false;
    }
}
