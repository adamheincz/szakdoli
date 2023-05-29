using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using TMPro;
using UnityEngine.Events;

public class PlayerController : NetworkBehaviour
{
    public Rigidbody2D rigidBody = null;
    public float moveSpeed = 5f;

    private string playerName = "";
    public MobilePlayer mobilePlayer;

    public Vector2 moveDirection;
    public Vector2 lookDirection;

    public UnityEvent<string> OnPlayerNameSet;

    private void Start()
    {
        MiniGameManager miniGameManager = FindObjectOfType<MiniGameManager>();
        Debug.Log("player_add_this: " + playerName);
        miniGameManager.AddPlayer(this);
        Debug.Log("minigame_manager_players" + miniGameManager.players);
    }

    public virtual void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    [ServerCallback]
    public virtual void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    [ServerCallback]
    public virtual void SetLookDirecton(Vector2 direction)
    {
        lookDirection = direction;
    }

    [ServerCallback]
    public void SetPlayerName(string name)
    {
        Debug.Log("playerName: " + name);
        playerName = name;
        OnPlayerNameSet?.Invoke(playerName);
    }

    public void SetSpawnTransform(Vector3 position, Quaternion rotation)
    {
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
    }

    public virtual void ExecuteAction()
    {

    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
