using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFire : MonoBehaviour {

    public GameObject specialFire;
    Rigidbody2D rb;
    public float firePower;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
}
