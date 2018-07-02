using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int Health = 100;
    }

    public PlayerStats playerStats = new PlayerStats();

    void Update()
    {
        if(transform.position.y <= -30)
        {
            DamagePlayer(999999);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.Health -= damage;
        if(playerStats.Health <= 0f)
        {
            GameController.KillPlayer(this);
        }
    }

}
