﻿
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public int playerMaxHealth; //starting health of enemy
    public int playerCurrentHealth; //current health of enemy
    bool damaged;
    public Image damageImage;
    public Color flashColor = new Color(1f, 0f, 0f, 0.3f);
    public float flashSpeed = 5f;

    public float invincibleTime = 1.5f;
    public bool isInvincible = false;
    Rigidbody2D rb;
    public SpriteRenderer[] sr;

    public Slider healthbar;
    public Text TxtHealth;


    private void Awake()
    {
        healthbar.value = 100;
        playerCurrentHealth = playerMaxHealth;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        
    }

    //public PlayerStats playerStats = new PlayerStats();

    void Update()
    {
        if(!isInvincible)
        {
            DamageFlash();
        }
        if(isInvincible)
        {
            DamageFlash();
            damaged = false;
        }
        //reset the damaged flag.
        damaged = false; 
        

        if (playerCurrentHealth <= 0)
        {
            Die();
        }

        if (transform.position.y <= -30)
        {
            HurtPlayer(playerCurrentHealth);
        }
    }

    public void SetInvincible()
    {
        isInvincible = true;
        CancelInvoke("SetDamageable"); // in case the method has already been invoked
        Invoke("SetDamageable", invincibleTime);
    }

    void SetDamageable()
    {
        isInvincible = false;
    }

    public void HurtPlayer(int damageToGive)
    {
        //Debug.Log("Hurt the player with " + damageToGive + " damage in hitpoints");
        
        damaged = true;

        if (damaged == true && !isInvincible == true)
        {
            playerCurrentHealth -= damageToGive;
            CalculateHealth();

        }
        
        //if (damaged == true && isInvincible == true)
        //{
            //Debug.Log("One of the spikes has hurt the player");
        //}
    }

    //used to have a calculate health function, get rid of
    float CalculateHealth()
    {
        TxtHealth.text = string.Format("{0} %", Mathf.RoundToInt(playerCurrentHealth));
        return healthbar.value = playerCurrentHealth;
        
    }

    public void Die()
    {
        HurtPlayer(playerCurrentHealth);
        Destroy(gameObject);
    }

    public void DamageFlash()
    {
        if (damaged)
        {
            //...set the color of the damageImage to the flash color.
            damageImage.color = flashColor;

        }
        //otherwise...
        else
        {
            //...transition the color back to clear
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    public IEnumerator InvulnFlash()
    {
        for (var n = 0; n < 5; n++)
        {

            sr[0].enabled = true;
            sr[1].enabled = true;
            sr[2].enabled = true;

            yield return new WaitForSeconds(.1f);

            sr[0].enabled = false;
            sr[1].enabled = false;
            sr[2].enabled = false;
            yield return new WaitForSeconds(.1f);

        }
        sr[0].enabled = true;
        sr[1].enabled = true;
        sr[2].enabled = true;
        yield break;
    }

    public IEnumerator Knockback(float knockDuration, float knockBackPower, Vector3 knockBackDirection)
    {
        float timer = 0f;
        //float xKnockBackPower = 150f;

        while (knockDuration > timer)
        {
            timer += Time.deltaTime;
            rb.AddForce(new Vector3(knockBackDirection.x, knockBackDirection.y + knockBackPower, transform.localPosition.z));
            StartCoroutine(InvulnFlash());
        }

        yield return 0;
        
    }
}
