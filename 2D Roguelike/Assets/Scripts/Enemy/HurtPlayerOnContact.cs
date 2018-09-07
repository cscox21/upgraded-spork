using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnitySampleAssets._2D
{ 
    public class HurtPlayerOnContact : MonoBehaviour
    {
        Player player;
        public int damageToGive;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Hit enemy need to be knocked back and take damage");
                collision.gameObject.GetComponent<Player>().HurtPlayer(damageToGive);

                Player playerScript = collision.gameObject.GetComponent<Player>();
                StartCoroutine(playerScript.Knockback(0f, 25f, playerScript.transform.localPosition));
                playerScript.SetInvincible();
                

                var player = collision.GetComponent<PlatformerCharacter2D>();
                player.knockbackCount = player.knockbackLength;

                if (collision.transform.position.x < transform.position.x)
                    player.knockFromRight = true;
                else
                    player.knockFromRight = false;
            }
        }
    }
}