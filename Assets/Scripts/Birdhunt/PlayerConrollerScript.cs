using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using TMPro;

public class PlayerConrollerScript : NetworkBehaviour
{
    public Rigidbody2D RigidBody;
    public PlayerInput playerControls;
    public HealthBarScript HealthBar;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    [SyncVar(hook = nameof(LoseHealth))]
    private int currentHealth;
    [SerializeField]
    private int damageTakenOnHit = 50;

    private int playerIndex;

    private string playerName = "player";


    public Vector2 moveDirection = Vector2.zero;


    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(currentHealth);
    }

    void Update()
    {

        DestroyIfDead();
    }

    private void FixedUpdate()
    {
        RigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnNameChanged(string oldName, string newName)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("take damage");
            LoseHealth(currentHealth, damageTakenOnHit);
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;

    }

    public void SetLookDirection()
    {
        if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-(Math.Abs(transform.localScale.x)), transform.localScale.y, transform.localScale.z);
        }
        else if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void SetPlayerIndex(int index)
    {
        playerIndex = index;
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;

        Debug.Log("playerName1: " + playerName);

        //playerName = "asdasd";

        Debug.Log("playerName2: " + playerName);

        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = playerName;
    }

    public void LoseHealth(int health, int lostHealth)
    {
        currentHealth -= lostHealth;
        HealthBar.SetHealth(currentHealth, maxHealth);
    }

    private void DestroyIfDead()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }
}
