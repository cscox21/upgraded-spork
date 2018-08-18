using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    Player player;
    //Rigidbody2D rb;
    public int damageToGive;

    void Start()
    {
        //get the Player script from the gameobject with the tag of "Player"
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            playerScript.SetInvincible();
            //Debug.Log("Player has hit the spikes and will take damage and fly up");
            other.gameObject.GetComponent<Player>().HurtPlayer(damageToGive);
            StartCoroutine(player.Knockback(.6f, 25f, player.transform.localPosition));

        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            playerScript.SetInvincible();

        }
    }
    */
}
