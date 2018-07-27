using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelNewArea : MonoBehaviour {

    public GameObject spwn1, spwn2;

	// Use this for initialization
	void Start ()
    {
        spwn1 = this.gameObject;
	}

    void OnTriggerStay2D(Collider2D trig)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            trig.gameObject.transform.position = spwn2.gameObject.transform.position;
        }
    }
}
