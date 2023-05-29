using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameToggle : MonoBehaviour
{
    [Header("MiniGame")]
    [SerializeField]
    private GameObject miniGame;
    [SerializeField]
    private GameObject miniGamePlayerPefab;
    [SerializeField]
    private Image background;
    [SerializeField]
    private TextMeshProUGUI label;
    [SerializeField]
    private Button createLobbyButton;
    [SerializeField]
    private Color activeLabelColor;
    [SerializeField]
    private Color inactiveLabelColor;

    public void ToggleChanged(bool value)
    {
        MiniGameToLevelManager(value);
        ChangeToggleOppacity(value);
    }

    private void ChangeToggleOppacity(bool value)
    {
        Color color = background.color;
        if (value)
        {
            color.a = 1.0f;
            label.color = activeLabelColor;
        }
        else
        {
            color.a = 0.5f;
            label.color = inactiveLabelColor;
        }

        background.color = color;
    }

    private void MiniGameToLevelManager(bool value)
    {
        if (value)
        {
            NetworkRoomManagerOwn.AddMiniGame((miniGame, miniGamePlayerPefab));
            
            createLobbyButton.interactable = true;
        }
        else
        {
            NetworkRoomManagerOwn.RemoveMiniGame((miniGame, miniGamePlayerPefab));

            if(NetworkRoomManagerOwn.miniGamesPlayers.Count == 0)
            {
                createLobbyButton.interactable = false;
            }
        }
    }
}
