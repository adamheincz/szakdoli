using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConrollerScript : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public InputAction PlayerControls;

    public float moveSpeed = 5f;

    Vector2 moveDirection = Vector2.zero;

    private int Health = 100;

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = PlayerControls.ReadValue<Vector2>();
        if (moveDirection.x == -1)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        } else if (moveDirection.x == 1)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        RigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void LoseHealth(int lostHealth)
    {
        Health -= lostHealth;
    }
}
