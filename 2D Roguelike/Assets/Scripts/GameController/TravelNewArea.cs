using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelNewArea : MonoBehaviour {

    public GameObject spwn1, spwn2;
    CameraSwitch camSwitch;
    //Player player;
    public bool canSwitchCam = false;

    private void Awake()
    {
        //player = FindObjectOfType<Player>();
        camSwitch = GameObject.FindGameObjectWithTag("GC").GetComponent<CameraSwitch>();
        //camSwitch = FindObjectOfType<CameraSwitch>().GetComponent<CameraSwitch>();
    }
    // Use this for initialization
    void Start ()
    {
        //camSwitch = GetComponent<CameraSwitch>();
        spwn1 = gameObject;
	}

    public void OnTriggerStay2D(Collider2D trig)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            canSwitchCam = true;
            Debug.Log("canSwitchCam is true");
            //camSwitch.switchCamera();
            trig.gameObject.transform.position = spwn2.gameObject.transform.position;
            //canSwitchCam = false;
        }
        
    }

}
