﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Use this for initialization
    void Start ()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            //clamps camera on x position
            float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
            //clamps camera on y position
            float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }

    }
}
