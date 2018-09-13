using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    public int healthToGive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null)
        {
            return;
        }

        Player playerScript = other.gameObject.GetComponent<Player>();
        other.gameObject.GetComponent<Player>().HurtPlayer(-healthToGive);

        Destroy(gameObject);
    }
}
