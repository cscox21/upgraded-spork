using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedEnemyShots : MonoBehaviour {

    [SerializeField]
    GameObject projectile;

    float fireRate;
    float nextFire;
    public float fireRange = 2f;
    public Transform target;

    //Trying multiple rays
    public float headHeight = 1f;
    public float sight = 3f;
    public float topSight = 5f;


	// Use this for initialization
	void Start ()
    {
        fireRate = 1f;
        nextFire = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position + Vector3.up * headHeight, transform.right * -sight, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * headHeight, (transform.right - transform.up).normalized * -topSight, Color.blue);
        CheckIFTimeToFire();
	}

    void CheckIFTimeToFire()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * headHeight, transform.right, -sight);
        RaycastHit2D topHit = Physics2D.Raycast(transform.position + Vector3.up * headHeight, (transform.right - transform.up).normalized, -topSight);

        if (hit.collider != null && hit.collider.tag == "Player") 
        {
            Debug.Log("Attack the Player");
            if (Time.time > nextFire)
            { 
                Instantiate(projectile, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }
        if (topHit.collider != null && topHit.collider.tag == "Player")
        {
            Debug.Log("Attack the Player with the angled raycast");
            if (Time.time > nextFire)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }
    }
}
