using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBat : MonoBehaviour {

    
    public float horizontalSpeed;   
    public float verticalSpeed;   
    public float amplitude;    

    public Transform[] patrolpoints;
    int currentPoint;

    private Vector3 tempPosition; 

    // Use this for initialization
    void Start ()
    {
        tempPosition = transform.position;

	}

    // Update is called once per frame
    void FixedUpdate()
    {
        tempPosition.x += horizontalSpeed;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;

        if (tempPosition.x < patrolpoints[currentPoint].position.x)
        {
            currentPoint++;
            transform.localScale = Vector3.one;
            Debug.Log("Going to point1");
            //horizontalSpeed = -horizontalSpeed;
            //tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
            //transform.position = tempPosition;
        }


        else if (tempPosition.x > patrolpoints[currentPoint].position.x)
        {
            currentPoint = 0;
            transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("Going to point2");
        //tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        //transform.position = tempPosition;
        }

    }

   
}
