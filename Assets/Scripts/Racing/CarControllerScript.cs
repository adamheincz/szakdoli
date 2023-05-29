using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class CarControllerScript : NetworkBehaviour
{
    public Rigidbody2D rigidBody;
    public PlayerInput playerControls;
    [SerializeField]
    private float driftAmount = 0.9f;
    [SerializeField]
    private float accelerationAmount = 20;
    [SerializeField]
    private float turnAmount = 9f;
    [SerializeField]
    private float maxSpeed = 10;

    private float rotationAngle = 0;
    private float velocityVsUp = 0;

    Vector2 moveDirection = Vector2.zero;

    private PlayerInput PlayerControls
    {
        get
        {
            if (playerControls != null)
            {
                return playerControls;
            }
            return playerControls = new PlayerInput();
        }
    }

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
        moveDirection = PlayerControls.PlayerMain.Move.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplyTireFriction();

        ApplySteering();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rigidBody.velocity);

        if (velocityVsUp > maxSpeed && moveDirection.y > 0)
            return;

        if (velocityVsUp < -maxSpeed * 0.5f && moveDirection.y < 0)
            return;

        if (rigidBody.velocity.sqrMagnitude > maxSpeed * maxSpeed && moveDirection.y > 0)
            return;

        if (moveDirection.y == 0)
        {
            rigidBody.drag = Mathf.Lerp(rigidBody.drag, 3, Time.deltaTime * 3);
        }
        Vector2 engineForceVector = transform.up * moveDirection.y * accelerationAmount;

        rigidBody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        float minSpeedBeforeTurning = Mathf.Clamp01(rigidBody.velocity.magnitude / 8);

        if(moveDirection.y >= 0)
        {
            rotationAngle -= moveDirection.x * turnAmount * minSpeedBeforeTurning;
        } else
        {
            rotationAngle -= -moveDirection.x * turnAmount * minSpeedBeforeTurning;
        }


        rigidBody.MoveRotation(rotationAngle);
    }

    private void ApplyTireFriction()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rigidBody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rigidBody.velocity, transform.right);

        rigidBody.velocity = forwardVelocity + rightVelocity * driftAmount;
    }

}
