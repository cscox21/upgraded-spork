using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour {
    
    float moveSpeed = 4f;

    Rigidbody2D rb;
    Player target;
    Vector2 moveDirection;
    public float fireBallSpeed = 2f;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * fireBallSpeed;
        Destroy(gameObject, 3f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }
}
