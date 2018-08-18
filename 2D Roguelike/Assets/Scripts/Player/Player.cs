
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

    public Color invulnColor;

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
        if (!isInvincible)
        {
            if (damaged)
            {
                Debug.Log("if Damaged...Set Flash color");
                //...set the color of the damageImage to the flash color.
                damageImage.color = flashColor;
                
                
            }
            //otherwise...
            else
            {
                //...transition the color back to clear
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
                //sr.color = Color.Lerp(sr.color, Color.clear, flashSpeed * Time.deltaTime);
            }
        }
        //reset the damaged flag.
        damaged = false; 
        

        if (playerCurrentHealth <= 0)
        {
            Die();
        }

        if (transform.position.y <= -30)
        {
            HurtPlayer(999999);
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
        Debug.Log("We are not longer invincible and will take damage");
        isInvincible = false;
    }

    public void HurtPlayer(int damageToGive)
    {
        //Debug.Log("Hurt the player with " + damageToGive + " damage in hitpoints");
        
        damaged = true;

        if (damaged == true && !isInvincible == true)
        {
            Debug.Log("One of the enemies has hurt the player");
            playerCurrentHealth -= damageToGive;
            CalculateHealth();

        }
        
        if (damaged == true && isInvincible == true)
        {
            Debug.Log("One of the spikes has hurt the player");

        }
        
    }

    //used to have a calculate health function, get rid of
    float CalculateHealth()
    {
        TxtHealth.text = string.Format("{0} %", Mathf.RoundToInt(playerCurrentHealth));
        return healthbar.value = playerCurrentHealth;
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public IEnumerator Knockback(float knockDuration, float knockBackPower, Vector3 knockBackDirection)
    {
        float timer = 0f;

        while(knockDuration > timer)
        {
            timer += Time.deltaTime;
            rb.AddForce(new Vector3(knockBackDirection.x, knockBackDirection.y + knockBackPower, transform.localPosition.z));

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
        yield return 0;
    }
}
