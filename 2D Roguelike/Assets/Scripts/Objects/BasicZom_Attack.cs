using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZom_Attack : MonoBehaviour {

    Player target;
    Rigidbody2D rb;
    public GameObject explosion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
    }


    void Start()
    {
        float xPower = Random.Range(300f, 450f);
        float yPower = Random.Range(300f, 350f);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log("Hit");
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "ground")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log("Hit the ground");
            Destroy(gameObject);
        }

    }
}


