using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPerspective : MonoBehaviour
{

	// Update is called once per frame
	void Update ()
    {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; // select distance = 10 units from the camera
            Debug.Log(Camera.main.ScreenToWorldPoint(mousePos)); 
    }
}
