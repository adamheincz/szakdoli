using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControllerScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float lifeTime = 5;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(bullet, transform.position, transform.rotation);
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        if(timer < lifeTime)
        {
            timer += Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }
    }
}
