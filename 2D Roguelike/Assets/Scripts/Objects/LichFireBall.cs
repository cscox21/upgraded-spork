﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichFireBall : MonoBehaviour
{
    Player target;
    Rigidbody2D rb;

    void Start()
    {
        float xPower = Random.Range(150f, 300f);
        float yPower = Random.Range(125f, 225f);
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();

            rb.AddForce(transform.up * yPower);
            rb.AddForce(transform.right * -xPower);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            Destroy(gameObject);
        }

        if (collision.collider.tag == "Player")
        {
            Debug.Log("SpecialFire Hit");
            Destroy(gameObject);
        }
    }
}
