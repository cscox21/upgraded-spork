
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
    float nextSpikeAttack;
    float spikeAttackRate;

    private AudioManager audioManager;
    public string dyingSound;
    public string hurtSound;

    private void Awake()
    {
        
        spikeAttackRate = 1.5f;
        nextSpikeAttack = Time.time;
        
    }

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        healthbar.value = playerCurrentHealth;
        sr = GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //caching
        audioManager = AudioManager.instance;       
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }
    }
    //public PlayerStats playerStats = new PlayerStats();

    void Update()
    {
        if(!isInvincible)
        {
            DamageFlash();
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
        
        if(Time.time> nextSpikeAttack)
        {
            nextSpikeAttack = Time.time + spikeAttackRate;
            Invoke("SetDamageable", invincibleTime);
        }
    }

    public void SetDamageable()
    {
        isInvincible = false;
    }

    public void HurtPlayer(int damageToGive)
    {
        //Debug.Log("Hurt the player with " + damageToGive + " damage in hitpoints");
        
        damaged = true;

        if (damaged == true && !isInvincible == true)
        {
            audioManager.PlaySound(hurtSound);
            playerCurrentHealth -= damageToGive;
            CalculateHealth();
        }
        
        if (damaged == true && isInvincible == true)
        {
            StartCoroutine(SpikeDamageFlash());
            //Debug.Log("One of the spikes has hurt the player");
        }
    }

    //used to have a calculate health function, get rid of
    float CalculateHealth()
    {
        
        TxtHealth.text = playerCurrentHealth + "/" + playerMaxHealth + " HP";
        //use as the second argument to the Format function above--> Mathf.RoundToInt(playerCurrentHealth));
        return healthbar.value = playerCurrentHealth;
        
    }

    public void Die()
    {
        HurtPlayer(playerCurrentHealth);
        audioManager.PlaySound(dyingSound);
        GameController.KillPlayer(this);
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
    
    public IEnumerator SpikeDamageFlash()
    {
        for (float timer = 0f; timer <1.5f ; timer++)
        {
            damageImage.color = flashColor;
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            yield return new WaitForSeconds(1.5f);
        }
        
    }
    
    public IEnumerator InvulnFlash()
    {
        for (var n = 0; n < 5; n++)
        {

            sr[0].enabled = true;
            sr[1].enabled = true;
            sr[2].enabled = true;
            sr[3].enabled = true;

            yield return new WaitForSeconds(.1f);

            sr[0].enabled = false;
            sr[1].enabled = false;
            sr[2].enabled = false;
            sr[3].enabled = false;
            yield return new WaitForSeconds(.1f);

        }
        sr[0].enabled = true;
        sr[1].enabled = true;
        sr[2].enabled = true;
        sr[3].enabled = true;
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
