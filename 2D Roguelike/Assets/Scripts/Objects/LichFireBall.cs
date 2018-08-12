using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichFireBall : MonoBehaviour
{
    Player target;
    Rigidbody2D rb;
    float speed = 10f;

    public float basicAttackRate = .25f;
    float nextBasicAttack;
    public GameObject projectile;
    public Transform firePos;

    void Start()
    {
        float xPower = Random.Range(-300f, 300f);
        float yPower = Random.Range(350f, 400f);
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
        nextBasicAttack = Time.time;

        rb.AddForce(transform.up * yPower);
            rb.AddForce(transform.right * -xPower);
    }

    private void Update()
    {
        transform.Rotate(0, 0, speed);
        LaunchProjectiles();

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

    void LaunchProjectiles()
    {
        if (Time.time > nextBasicAttack)
        {
            Instantiate(projectile, firePos.position, Quaternion.identity);
            nextBasicAttack = Time.time + basicAttackRate;
        }
    }
}

