using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerMaxHealth; //starting health of enemy
    public int playerCurrentHealth; //current health of enemy
    SpriteRenderer spriteRenderer;
    Color hurtColor = Color.red;
    Color normalColor;

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        StartCoroutine(Damaged());
    }
    public void MaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public IEnumerator Damaged()
    {
        for(int i = 0; i < 5; i++)
        {
            spriteRenderer.color = hurtColor;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.color = normalColor;
            yield return new WaitForSeconds(.1f);
        }
    }
}
