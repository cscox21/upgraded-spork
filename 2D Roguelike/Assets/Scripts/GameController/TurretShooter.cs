﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour {

    
    public GameObject projectile;
    public float speedFactor;
    public float delay;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Shoots());
	}

    IEnumerator Shoots()
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().velocity = -transform.right * speedFactor;
        }
    }
}
