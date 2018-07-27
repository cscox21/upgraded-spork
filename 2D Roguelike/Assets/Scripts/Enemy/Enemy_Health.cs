using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    public int enemyMaxHealth; //starting health of enemy
    public int enemyCurrentHealth; //current health of enemy
    public int scoreValue; //how many points killing an enemy gives


    private void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    void Update ()
    {
        if(enemyCurrentHealth<=0)
        {
            Destroy(gameObject);
        }
	    if(gameObject.transform.position.y <-10)
        {
            Destroy(gameObject);
        }
	}

    public void HurtEnemy(int damageToGive)
    {
        enemyCurrentHealth -= damageToGive;
    }
    public void MaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

}
