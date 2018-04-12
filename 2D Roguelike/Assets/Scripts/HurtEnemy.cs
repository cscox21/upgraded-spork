using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

    public int damageToGive;
    public GameObject damageBurst;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="Enemy")
        {
            other.gameObject.GetComponent<Enemy_Health>().HurtEnemy(damageToGive);
            Instantiate(damageBurst, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
