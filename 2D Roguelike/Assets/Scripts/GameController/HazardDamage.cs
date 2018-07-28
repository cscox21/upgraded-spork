using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    Player player;
    //Rigidbody2D rb;
    public int damageToGive;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has hit the spikes and will take damage and fly up");
            other.gameObject.GetComponent<Player>().HurtPlayer(damageToGive);
            StartCoroutine(player.Knockback(.5f, 25f, player.transform.position));
        }
    }

}
