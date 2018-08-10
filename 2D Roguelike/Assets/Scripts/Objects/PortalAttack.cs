using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAttack : MonoBehaviour {

    Animator anim;
    public GameObject portProjectile;
    float nextBasicAttack;
    public float basicAttackRate = 1f;
    public Transform firePos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start ()
    {
        nextBasicAttack = Time.time;
        anim.SetBool("isAttacking", false);
        StartCoroutine(SpawnFireball());
    }

    IEnumerator SpawnFireball()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(3f);
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(1f);
            Shoot();
            Destroy(gameObject);
            yield return null;
        }
    }

    void Shoot()
    {
            Debug.Log("Shooting the portal projetiles right now");
            Instantiate(portProjectile, firePos.position, Quaternion.identity);
    }
    
}
