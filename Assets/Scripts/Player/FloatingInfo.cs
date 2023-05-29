using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingInfo : MonoBehaviour
{
    [SerializeField]
    private Vector2 offset;

    private void FixedUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(transform.GetComponentInParent<Rigidbody2D>().position + offset);

    }
}
