using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour {

    public float moveSpeed = 4f;
    //public float fireBallSpeed = 7f;
    Rigidbody2D rb;
    Player target;
    Vector2 moveDirection;
    bool facingRight = false;
    public GameObject explosion;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
    }
    // Use this for initialization
    void Start ()
    {        
        moveDirection = (target.transform.position - transform.position).normalized* moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 2f);

        if (target.transform.position.x > transform.position.x)
        {
            Flip();
        }
    }

    private void Update()
    {
        if(target ==null)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player")
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
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
