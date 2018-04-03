using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootFireball : MonoBehaviour {

    public float speed;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
