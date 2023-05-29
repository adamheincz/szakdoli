using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayerController : PlayerController
{
    [SerializeField]
    private float rotationSpeed = 50f;

    public bool isRotationLocked = false;

    public override void FixedUpdate()
    {
        moveDirection.Normalize();
        rigidBody.MovePosition(rigidBody.position + moveDirection * moveSpeed * Time.deltaTime);

        if (lookDirection != Vector2.zero && !isRotationLocked)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, lookDirection);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rigidBody.MoveRotation(rotation);
        }
    }

    public override void ExecuteAction()
    {
        Debug.Log("shoot");
        //gameObject.GetComponent<Shooting>().Shoot();
        StartCoroutine(gameObject.GetComponent<Laser>().Shoot());
    }

}
