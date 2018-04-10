using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth;
    public int playerCurrentHealth;


    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update ()
    {
        if(playerCurrentHealth<=0)
        {
            gameObject.SetActive(false);
        }
		if(gameObject.transform.position.y < -7)
        {
            Die();
        }
	}
    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
    void Die()
    {
        SceneManager.LoadScene("Level_01");       
    }
}
