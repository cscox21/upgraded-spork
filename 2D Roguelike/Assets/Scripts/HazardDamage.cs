using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    Rigidbody2D rb;
    public int damageToGive;
    public float bounceHeight = 400f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().HurtPlayer(damageToGive);
            rb.AddForce(other.transform.up * bounceHeight);
        }
    }
}
