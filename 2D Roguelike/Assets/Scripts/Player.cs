using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerMaxHealth; //starting health of enemy
    public int playerCurrentHealth; //current health of enemy
    //[System.Serializable]
    //public class PlayerStats
    //{
    //public int Health = 100;
    //}

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

    //public void DamagePlayer(int damage)
    //{
    //playerStats.Health -= damage;
    //if(playerStats.Health <= 0f)
    //{
    //GameController.KillPlayer(this);
    //}
    //}

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }
    public void MaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
