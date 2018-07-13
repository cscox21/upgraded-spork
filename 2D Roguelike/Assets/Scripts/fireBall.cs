﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour {

    float moveSpeed = 4f;

    Rigidbody2D rb;
    Player target;
    Vector2 moveDirection;
    bool facingRight = false;


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized* moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);

        if (target.transform.position.x > transform.position.x)
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
