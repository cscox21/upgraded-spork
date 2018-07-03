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
    public float fovAngle;
    public float sight = 3f;

	// Use this for initialization
	void Start ()
    {
        fireRate = 1f;
        nextFire = Time.time;
       


        //Vector3 rayPosition = new Vector3(transform.position.x, headHeight, transform.position.z);
        //Vector3 leftRayRotation = Quaternion.AngleAxis(-fovAngle, transform.up) * transform.forward;
        //Vector3 rightRayRotation = Quaternion.AngleAxis(fovAngle, transform.up) * transform.forward;

        //Constructing rays

    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position + Vector3.left * headHeight, transform.right * -sight, Color.green);
        Debug.DrawRay(transform.position + Vector3.left * headHeight, (transform.right + transform.up).normalized * -sight, Color.blue);
        Debug.DrawRay(transform.position + Vector3.left * headHeight, (transform.right - transform.up).normalized * -sight, Color.blue);
        CheckIFTimeToFire();
	}

    void CheckIFTimeToFire()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);




        //Vector3 rayPosition = new Vector3(transform.position.x, headHeight, transform.position.z);
        //Vector3 leftRayRotation = Quaternion.AngleAxis(-fovAngle, transform.up) * transform.forward;
        //Vector3 rightRayRotation = Quaternion.AngleAxis(fovAngle, transform.up) * transform.forward;

        //Ray rayCenter = new Ray(rayPosition, transform.forward);
        //Ray rayLeft = new Ray(rayPosition, leftRayRotation);
        //Ray rayRight = new Ray(rayPosition, rightRayRotation);

        if (Time.time > nextFire) //&& rayCenter.collider.tag == "Player")
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
