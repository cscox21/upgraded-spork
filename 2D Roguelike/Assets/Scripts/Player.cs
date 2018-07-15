
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerMaxHealth; //starting health of enemy
    public int playerCurrentHealth; //current health of enemy
    bool damaged;
    public Image damageImage;
    public Color flashColour = new Color(1f, 0f, 0f, 0.3f);
    public float flashSpeed = 5f;

    private void Start()
    {
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

    public void HurtPlayer(int damageToGive)
    {
        Debug.Log("hurt the player with " + damageToGive + " damage in hitpoints");
        damaged = true;
        playerCurrentHealth -= damageToGive;
    }
    public void MaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    
}
