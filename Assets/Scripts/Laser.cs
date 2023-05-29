using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer laserRenderer;
    [SerializeField]
    private int numOfReflections;
    [SerializeField]
    private float maxRayDistance;
    [SerializeField]
    private LayerMask layerDetection;
    [SerializeField]
    private float f = 0.1f;
    [SerializeField]
    private float laserActiveTime = 1f;
    [SerializeField]
    private float laserCooldown = 1f;
    [SerializeField]
    private float scoreOnHit = 25f;
    [SerializeField]
    private float scoreOnKill = 100f;

    private float remainingRayDistance;

    private RaycastHit2D hit;
    private float currentCooldown = 0f;
    private bool canShoot = true;

    public UnityEvent<float> OnCharging;
    
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        OnCharging?.Invoke(currentCooldown);
    }

    private void Update()
    {

        if(!canShoot)
        {
            currentCooldown -= Time.deltaTime;
            OnCharging?.Invoke(currentCooldown / laserCooldown);
            if(currentCooldown <= 0)
            {
                canShoot = true;
            }
        }


        if(laserRenderer.enabled)
        {
            laserRenderer.positionCount = 1;
            laserRenderer.SetPosition(0, transform.position);
            remainingRayDistance = maxRayDistance;

            Ray2D ray = new Ray2D(transform.position, transform.up);

            for (int i = 0; i < numOfReflections; i++)
            {
                hit = Physics2D.Raycast(ray.origin, ray.direction, remainingRayDistance, layerDetection);


                if (hit)
                {
                    laserRenderer.positionCount += 1;

                    laserRenderer.SetPosition(laserRenderer.positionCount - 1, hit.point + ray.direction * f);
                    remainingRayDistance -= Vector2.Distance(ray.origin, hit.point);
                    ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));
                    
                    if (!hit.collider.CompareTag("Mirror"))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            DoDamage(10);
                        }
                        break;
                    }
                }
                else
                {
                    laserRenderer.positionCount += 1;

                    laserRenderer.SetPosition(laserRenderer.positionCount - 1, ray.origin + ray.direction * remainingRayDistance);
                }

            }

        }

    }

    public IEnumerator Shoot()
    {
        if(canShoot)
        {
            canShoot = false;
            currentCooldown = laserCooldown;

            laserRenderer.enabled = true;
            gameObject.GetComponent<ShooterPlayerController>().isRotationLocked = true;
            yield return new WaitForSeconds(laserActiveTime);
            laserRenderer.enabled = false;
            gameObject.GetComponent<ShooterPlayerController>().isRotationLocked = false;
        }
    }

    private void DoDamage(float damage)
    {
        hit.rigidbody.GetComponentInParent<Health>().LoseHealth(damage);
        if(hit.rigidbody != gameObject.GetComponent<Rigidbody2D>())
        {
            if (!hit.rigidbody.GetComponentInParent<Health>().isDead)
            {
                gameObject.GetComponent<Score>().AddScore(scoreOnHit);
            }
            else
            {
                gameObject.GetComponent<Score>().AddScore(scoreOnKill);
            }
        }
        //StartCoroutine(hit.rigidbody.GetComponentInParent<ShooterPlayerController>().Invulnerability());
    }


}
