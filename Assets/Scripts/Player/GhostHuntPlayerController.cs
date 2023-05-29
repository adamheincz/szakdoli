using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostHuntPlayerController : PlayerController
{
    [SerializeField]
    private int damageTakenOnHit = 50;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameObject.GetComponent<Health>().LoseHealth(damageTakenOnHit);
        }
    }

}
