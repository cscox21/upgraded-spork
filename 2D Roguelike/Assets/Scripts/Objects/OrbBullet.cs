using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBullet : MonoBehaviour
{
    public float moveSpeed = 300f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        rb.AddForce(transform.forward * moveSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        
        Destroy(gameObject, .4f);
    }
}
