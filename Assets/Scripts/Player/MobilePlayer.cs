using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System;

public class MobilePlayer : NetworkBehaviour
{
    GameObject player = null;

    public string mobilePlayerName = "mobile_player";

    [ClientCallback]
    public void OnMove(InputValue value)
    {
        CmdMove(value.Get<Vector2>());
    }

    [ClientCallback]
    public void OnLook(InputValue value)
    {
        CmdLook(value.Get<Vector2>());
    }

    [ClientCallback]
    public void OnAction(InputValue value)
    {
        CmdExecuteAction();
    }

    [ClientRpc]
    public void RpcSpawnPlayer(int miniGameIndex, Vector2 startPosition, Quaternion startRotation)
    {
        Debug.Log("mini_game_loading");
        CmdSpawnPlayer(miniGameIndex, startPosition, startRotation);
    }

    [Command]
    public void CmdSpawnPlayer(int miniGameIndex, Vector2 startPosition, Quaternion startRotation)
    {
        Debug.Log("cmd_spawn_player, minigameindex: " + miniGameIndex);
        player = Instantiate(NetworkRoomManagerOwn.miniGamesPlayers[miniGameIndex].Item2);
        Debug.Log("set_player_name: " + mobilePlayerName);
        player.GetComponent<PlayerController>().SetPlayerName(mobilePlayerName);

        NetworkServer.Spawn(player);

        Debug.Log("spawn_point: " + startPosition);
        player.GetComponent<PlayerController>().SetSpawnTransform(startPosition, startRotation);
        player.GetComponent<PlayerController>().mobilePlayer = this;
        player.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    [Command]
    private void CmdMove(Vector2 direction)
    {
        if (player != null)
        {
            Debug.Log("move");
            player.GetComponent<PlayerController>().SetMoveDirection(direction);
        }
    }

    [Command]
    private void CmdLook(Vector2 direction)
    {
        if (player != null)
        {
            player.GetComponent<PlayerController>().SetLookDirecton(direction);
        }
    }

    [Command]
    private void CmdExecuteAction()
    {
        if (player != null)
        {
            player.GetComponent<PlayerController>().ExecuteAction();
        }
    }

    [ServerCallback]
    public void SetMobilePlayerName(string name)
    {
        mobilePlayerName = name;
        Debug.Log("mobile player set to: " + mobilePlayerName);
    }

}
