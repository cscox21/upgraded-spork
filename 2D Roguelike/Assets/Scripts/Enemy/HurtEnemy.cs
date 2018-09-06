using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

    public int damageToGive;
    public GameObject damageBurst;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="Enemy")
        {
            other.gameObject.GetComponent<Enemy_Health>().HurtEnemy(damageToGive);
            Instantiate(damageBurst, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "ground" || other.gameObject.tag == "MovingPlatform")
        {
            Destroy(gameObject);
        }
    }
}
