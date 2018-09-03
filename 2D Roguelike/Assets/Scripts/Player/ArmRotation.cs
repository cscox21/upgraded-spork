using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 0;

    void Update ()
    {
        
        //subtracting the position of the player from the mouse position.
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //var ray = Camera.main.ScreenPointToRay(Input.GetTouch().position);

        difference.Normalize();  //normailizing the vector. Meaning that all the sum of the vector will be equal to 1.

        //find the angle in degrees
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //Debug.Log(rotZ);

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        
    }
}
