﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public float destroyTime;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
