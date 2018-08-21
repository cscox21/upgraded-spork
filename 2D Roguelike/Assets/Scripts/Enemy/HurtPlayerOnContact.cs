using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnitySampleAssets._2D
{ 
    public class HurtPlayerOnContact : MonoBehaviour
    {

        public int damageToGive;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Hit enemy need to be knocked back and take damage");
                collision.gameObject.GetComponent<Player>().HurtPlayer(damageToGive);

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