using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootScript : MonoBehaviour
{
    public float moveSpeed = 4;
    public float deadZone = 6;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;

        if(transform.position.x > deadZone)
        {
            Destroy(gameObject);
        }
    }
}
