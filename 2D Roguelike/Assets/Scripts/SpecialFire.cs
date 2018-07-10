using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFire : MonoBehaviour {

    Player target;
    Rigidbody2D rb;
    public Transform boss;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
        if (target.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-3f, 3.5f);
        }
        else
        {
            rb.velocity = new Vector2(3f, 3.5f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
}
