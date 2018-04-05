using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue;

    bool isDead;
    BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentHealth = startingHealth;
    }

    void Update ()
    {
	    if(gameObject.transform.position.y <-10)
        {
            Destroy(gameObject);
        }
	}

    public void TakeDamage(int amount, Vector2 hitPoint)
    {
        if(isDead)
        
            return;
        currentHealth -= amount;
        if(currentHealth<=0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        boxCollider2D.isTrigger = true;
        Destroy(gameObject, 2f);
    }

}
