using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerController : PlayerController
{

    [SerializeField]
    private float driftAmount = 0.5f;
    [SerializeField]
    private float accelerationAmount = 20f;
    [SerializeField]
    private float turnAmount = 9f;
    [SerializeField]
    private float maxSpeed = 10;

    private float velocityVsUp = 0;

    public override void FixedUpdate()
    {
        ApplyEngineForce();

        ApplyTireFriction();

        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(moveDirection.normalized, rigidBody.velocity);

        if (velocityVsUp > maxSpeed)
            return;

        if (velocityVsUp < -maxSpeed * 0.5f)
            return;

        if (rigidBody.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            return;

        if (moveDirection.normalized == Vector2.zero)
        {
            rigidBody.drag = Mathf.Lerp(rigidBody.drag, 3, Time.deltaTime * 3);
        }
        Vector2 engineForceVector = moveDirection.normalized * accelerationAmount;

        rigidBody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {

        if (moveDirection != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, moveDirection);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnAmount  * Time.deltaTime);

            rigidBody.MoveRotation(rotation);
        }
    }

    private void ApplyTireFriction()
    {
        Vector2 rightDirection = new Vector2(moveDirection.y, -moveDirection.x);
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rigidBody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rigidBody.velocity, transform.right);

        rigidBody.velocity = forwardVelocity + rightVelocity * driftAmount;
    }
}
