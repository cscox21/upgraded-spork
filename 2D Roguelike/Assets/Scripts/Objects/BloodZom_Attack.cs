using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodZom_Attack : MonoBehaviour {

    Player target;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
    }

    void Start()
    {
        
        float xPower = Random.Range(300f, 450f);
        float yPower = Random.Range(125f, 250f);

        

        
        if (target.transform.position.x > transform.position.x)
        {
            rb.AddForce(transform.up * yPower);
            rb.AddForce(transform.right * xPower);
        }
        else
        {
            rb.AddForce(transform.up * yPower);
            rb.AddForce(transform.right * -xPower);
        }

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
