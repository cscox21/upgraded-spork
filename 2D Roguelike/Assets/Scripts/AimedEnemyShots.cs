using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedEnemyShots : MonoBehaviour {

    [SerializeField]
    GameObject projectile;

    float fireRate;
    float nextFire;

	// Use this for initialization
	void Start ()
    {
        fireRate = 1f;
        nextFire = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckIFTimeToFire();
	}

    void CheckIFTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
