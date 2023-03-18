using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControllerScript : MonoBehaviour
{
    public GameObject bullet;
    public float lifeTime = 5;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(bullet, transform.position, transform.rotation);
        Instantiate(bullet, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), transform.rotation);
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
