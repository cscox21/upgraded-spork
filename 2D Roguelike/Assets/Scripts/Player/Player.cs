
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public int playerMaxHealth; //starting health of enemy
    public int playerCurrentHealth; //current health of enemy
    bool damaged;
    public Image damageImage;
    public Color flashColour = new Color(1f, 0f, 0f, 0.3f);
    public float flashSpeed = 5f;

    public float invincibleTime = 1f;
    public bool isInvincible = false;
    Rigidbody2D rb;

    public Slider healthbar;
    private void Awake()
    {
        healthbar.value = CalculateHealth();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCurrentHealth = playerMaxHealth;
        
    }

    //public PlayerStats playerStats = new PlayerStats();

    void Update()
    {
        if (damaged)
        {
            //...set the color of the damageImage to the flash color.
            damageImage.color = flashColour;
        }
        //otherwise...
        else
        {
            //...transition the color back to clear
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        //reset the damaged flag.
        damaged = false;
        

        if (playerCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (transform.position.y <= -30)
        {
            HurtPlayer(999999);
        }
    }

    public void SetInvincible()
    {
        isInvincible = true;
        Debug.Log("We are invincible now!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        CancelInvoke("SetDamageable"); // in case the method has already been invoked
        Invoke("SetDamageable", invincibleTime);
    }

    void SetDamageable()
    {
        isInvincible = false;
    }

    public void HurtPlayer(int damageToGive)
    {
        Debug.Log("hurt the player with " + damageToGive + " damage in hitpoints");
        damaged = true;
        
        if (damaged == true && !isInvincible == true)
        {
            playerCurrentHealth -= damageToGive;
            healthbar.value = CalculateHealth();
        }
        if(damaged == true && isInvincible == true)
        {
            playerCurrentHealth -= damageToGive;
            healthbar.value = CalculateHealth();
        }
    }
    public void MaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    float CalculateHealth()
    {
        return playerCurrentHealth / playerMaxHealth;
    }

    public IEnumerator Knockback(float knockDuration, float knockBackPower, Vector3 knockBackDirection)
    {
        float timer = 0f;

        while(knockDuration > timer)
        {
           timer += Time.deltaTime;
           rb.AddForce(new Vector3(knockBackDirection.x, knockBackDirection.y + knockBackPower, transform.localPosition.z));   
        }
        yield return 0;
    }
}
