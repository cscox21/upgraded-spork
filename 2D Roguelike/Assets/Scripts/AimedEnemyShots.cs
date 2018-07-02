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
    public float headHeight;
    public float fovAngle;

	// Use this for initialization
	void Start ()
    {
        fireRate = 1f;
        nextFire = Time.time;
        Vector3 rayPosition = new Vector3(transform.position.x, headHeight, transform.position.z);
        Vector3 leftRayRotation = Quaternion.AngleAxis(-fovAngle, transform.up) * transform.forward;
        Vector3 rightRayRotation = Quaternion.AngleAxis(fovAngle, transform.up) * transform.forward;

        //Constructing rays
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckIFTimeToFire();
	}

    void CheckIFTimeToFire()
    {
        Vector3 rayPosition = new Vector3(transform.position.x, headHeight, transform.position.z);
        Vector3 leftRayRotation = Quaternion.AngleAxis(-fovAngle, transform.up) * transform.forward;
        Vector3 rightRayRotation = Quaternion.AngleAxis(fovAngle, transform.up) * transform.forward;

        Ray rayCenter = new Ray(rayPosition, transform.forward);
        Ray rayLeft = new Ray(rayPosition, leftRayRotation);
        Ray rayRight = new Ray(rayPosition, rightRayRotation);

        if (Time.time > nextFire && rayCenter.collider.tag == "Player")
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
