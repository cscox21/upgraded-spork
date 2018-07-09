using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFire : MonoBehaviour {

    Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-3f,3.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "ground")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
}
