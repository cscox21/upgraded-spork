using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : MonoBehaviour {

    Rigidbody2D rb;


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "ground")
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce (new Vector2(0, 15), ForceMode2D.Impulse);
        }

        if(gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
