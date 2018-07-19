using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdAttack : MonoBehaviour {

    public GameObject lightExplosion;
    public float moveSpeed = 30f;

    private void Awake()
    {
        transform.Rotate(0f, 0f, -90f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, .4f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "ground")
        {
            Instantiate(lightExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
