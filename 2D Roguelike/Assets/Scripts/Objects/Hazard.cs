using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    Player player;
    Collider2D spikeCol;

    public int damageToGive;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    /*void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }*/

    private void Update()
    {
        if(player ==null)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            other.gameObject.GetComponent<Player>().HurtPlayer(damageToGive);
            StartCoroutine(player.Knockback(.6f, 25f, player.transform.localPosition));
            playerScript.SetInvincible();

        }
    }
}
