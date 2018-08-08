﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 90;
	// Update is called once per frame
	void Update ()
    {
        //subtracting the position of the player from the mouse position.
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;   
        difference.Normalize();  //normailizing the vector. Meaning that all the sum of the vector will be equal to 1.

        //find the angle in degrees
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        
    }
}
