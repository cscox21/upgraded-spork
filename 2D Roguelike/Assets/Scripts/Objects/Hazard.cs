using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            playerScript.SetInvincible();
            //Destroy(other.gameObject);

        }
    }
}
