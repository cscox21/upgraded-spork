using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headless_Fireball : MonoBehaviour {

    Player target;
    Rigidbody2D rb;

        
    void Start()
    {
        float xPower = Random.Range(-200f, 200f);
        float yPower = Random.Range(125f, 400f);

        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();

        if (target.transform.position.x < transform.position.x)
        {
            rb.AddForce(transform.up * yPower);
            rb.AddForce(transform.right * -xPower);
        }
        else
        {
            rb.AddForce(transform.up * yPower);
            rb.AddForce(transform.right * xPower);
        }
    }

    private void FixedUpdate()
    {
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
