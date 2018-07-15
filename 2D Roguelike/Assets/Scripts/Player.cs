using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerMaxHealth; //starting health of enemy
    public int playerCurrentHealth; //current health of enemy

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    //public PlayerStats playerStats = new PlayerStats();

    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (transform.position.y <= -30)
        {
            HurtPlayer(999999);
        }
    }

    public void HurtPlayer(int damageToGive)
    {
        Debug.Log("hurt the player with " + damageToGive + " damage in hitpoints");

        playerCurrentHealth -= damageToGive;
    }
    public void MaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
