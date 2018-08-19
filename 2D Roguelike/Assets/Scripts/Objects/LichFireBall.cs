using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichFireBall : MonoBehaviour
{
    Player target;
    Rigidbody2D rb;
    float speed = 10f;

    public GameObject projectile;
    public Transform firePos;
    public float delay;
    public float moveSpeed;
    public GameObject explosion;

    void Start()
    {
        float xPower = Random.Range(-300f, 300f);
        float yPower = Random.Range(350f, 400f);
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();

        rb.AddForce(transform.up * yPower);
        rb.AddForce(transform.right * -xPower);
        StartCoroutine(Shoots());
    }

    private void Update()
    {
        transform.Rotate(0, 0, speed);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "ground")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator Shoots()
    {

        while (true)
        {
            yield return new WaitForSeconds(delay);
            GameObject clone = Instantiate(projectile, firePos.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().velocity = -transform.right * moveSpeed;
        }       
    }
}

