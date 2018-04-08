using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    public int startingHealth = 100; //starting health of enemy
    public int currentHealth; //current health of enemy
    public int scoreValue; //how many points killing an enemy gives

    bool isDead; //whether enemy is dead
    BoxCollider2D boxCollider2D; //reference to box collider 2D

    private void Awake()
    {
        //sets up the references
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
        //if the enemy is dead...
        if(isDead)
        
            //...no need to take damage so exit the function
            return;

        //reduce current health by amoutn of damage sustained
        currentHealth -= amount;
        //if the current health is less than or equal to zero...
        if(currentHealth<=0)
        {
            //...the enemy is dead.
            Death();
        }
    }

    void Death()
    {
        //the enemy is dead
        isDead = true;
        //turn collider intoa  trigger so shots can pass through it.
        boxCollider2D.isTrigger = true;
        //destroy gameobject after 2 seconds
        Destroy(gameObject, 2f);
    }

}
