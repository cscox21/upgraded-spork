using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelNewArea : MonoBehaviour {

    public GameObject spwn1, spwn2;
    CameraSwitch camSwitch;

    private void Awake()
    {
        camSwitch = GetComponent<CameraSwitch>();
        //camSwitch = FindObjectOfType<CameraSwitch>().GetComponent<CameraSwitch>();
    }
    // Use this for initialization
    void Start ()
    {
        //camSwitch = GetComponent<CameraSwitch>();
        spwn1 = gameObject;
	}

    void OnTriggerStay2D(Collider2D trig)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            camSwitch.switchCamera();
            trig.gameObject.transform.position = spwn2.gameObject.transform.position;
            
        }
    }

}
