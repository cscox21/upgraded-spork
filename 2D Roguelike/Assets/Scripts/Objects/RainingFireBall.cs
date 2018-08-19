using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainingFireBall : MonoBehaviour
{

    public float moveSpeed = 4f;
    Rigidbody2D rb;
    public GameObject explosion;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        transform.Rotate(0f, 0f, 90f);
        rb.AddForce(transform.right * -moveSpeed );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "ground")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }

}
