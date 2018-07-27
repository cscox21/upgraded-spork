using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().HurtPlayer(damageToGive);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
}
