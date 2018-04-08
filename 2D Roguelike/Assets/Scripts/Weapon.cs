using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
    public enum Modes 
    { Melee, Straight, Follow, Throw}

    public Sprite sprite; //reference to sprite image
    public GameObject projectile; //reference to projectile
    public float projectileSpeed; //velocity of projectiles
    public float coolDown; //time between attacks 
    public Modes projectileMode;


    public int damagePerShot = 50;

    // Use this for initialization
    void Start ()
    {

	}


}
