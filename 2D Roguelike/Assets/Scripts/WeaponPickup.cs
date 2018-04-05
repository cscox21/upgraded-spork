using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public float coolDown = 2f;
    float counter;
    public GameObject[] weapons;
    public GameObject weaponHere;
    bool caught;



	// Use this for initialization
	void Start ()
    {
        weaponHere = weapons[Random.Range(0, weapons.Length)];
        GetComponent<SpriteRenderer>().sprite = weaponHere.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //is going to reset the weapon pickup so you can pickup another weapon after some time
		if(caught)
        
            counter += Time.deltaTime;
        if(counter>=coolDown)
        {
            caught = false;
            counter = 0; //sets counter back to zero
            weaponHere = weapons[Random.Range(0, weapons.Length)];
            GetComponent<SpriteRenderer>().sprite = weaponHere.GetComponent<SpriteRenderer>().sprite;
        }
        
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the player runs into the collider on Weapon Pickup, then equips new weapon
        if(other.tag=="Player")
        {
            other.transform.Find("WeaponSlot").GetComponent<WeaponManager>().UpdateWeapon(weaponHere);
            caught = true;
            GetComponent<SpriteRenderer>().sprite = null;
        }

    }
}
