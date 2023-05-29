using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] playerNames;
    [SerializeField]
    private TextMeshProUGUI[] playerScores;
    [SerializeField]
    private Image[] playerAvatars;
    [SerializeField]
    private TextMeshProUGUI countDownText;
    [SerializeField]
    private int secondsUntilNextGame = 5;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        StartCoroutine(CountDownNextGame());
    }

    public void SetPlayerInfos(List<string> names, List<Sprite> sprites, List<float> scores)
    {
        for (int i = 0; i < names.Count; i++)
        {
            playerNames[i].text = names[i];
            playerScores[i].text = scores[i].ToString();
            playerAvatars[i].sprite = sprites[i];
            playerAvatars[i].SetNativeSize();
        }
    }

    IEnumerator CountDownNextGame()
    {
        while(secondsUntilNextGame > 0)
        {
            countDownText.text = "NEXT GAME IN " + secondsUntilNextGame.ToString();

            yield return new WaitForSeconds(1);

            secondsUntilNextGame--;
        }

        levelManager.LoadNextMiniGame();

        Destroy(gameObject);
    }
}
