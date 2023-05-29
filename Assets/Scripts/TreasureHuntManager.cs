using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreasureHuntManager : MiniGameManager
{
    [SerializeField]
    private int secondsUntilGameOver = 30;

    [SerializeField]
    private TextMeshProUGUI countDownText;

    public override void StartMiniGame()
    {
        base.StartMiniGame();
        StartCoroutine(CountDown());
    }

    public override bool EndCondition()
    {
        if(secondsUntilGameOver <= 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator CountDown()
    {
        while (secondsUntilGameOver >= 0)
        {
            countDownText.text = secondsUntilGameOver.ToString();

            yield return new WaitForSeconds(1);

            secondsUntilGameOver--;
        }
    }


}
