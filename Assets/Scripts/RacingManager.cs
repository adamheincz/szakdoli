using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingManager : MiniGameManager
{
    public override bool EndCondition()
    {
        if (players.Count != 0)
        {
            int finishedCounter = 0;

            foreach (PlayerController player in players)
            {
                if (player.gameObject.GetComponent<CarLapCounter>().isRaceCompleted == true)
                {
                    finishedCounter++;
                }
            }

            if (finishedCounter == players.Count)
            {
                return true;
            }

            return false;
        }
        return false;
    }
}
