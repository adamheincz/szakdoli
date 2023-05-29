using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunterPlayerController : PlayerController
{
    [SerializeField]
    private float rotationSpeed = 360f;

    public override void FixedUpdate()
    {
        moveDirection.Normalize();
        rigidBody.MovePosition(rigidBody.position + moveDirection * moveSpeed * Time.deltaTime);

        if (lookDirection != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, lookDirection);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rigidBody.MoveRotation(rotation);
        }
    }
}
