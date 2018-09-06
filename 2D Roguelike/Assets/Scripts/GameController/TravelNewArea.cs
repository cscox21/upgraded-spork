using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelNewArea : MonoBehaviour {

    public GameObject spwn1, spwn2;
    CameraSwitch camSwitch;
    public bool canSwitchCam = false;

    private void Awake()
    {
        //camSwitch = GameObject.FindGameObjectWithTag("GC").GetComponent<CameraSwitch>();
    }
    // Use this for initialization
    void Start ()
    {
        spwn1 = gameObject;
	}

    public void OnTriggerStay2D(Collider2D trig)
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Go to Boss level 1");
            int scene = SceneManager.GetActiveScene().buildIndex +1;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);

            //canSwitchCam = true;
            
            //trig.gameObject.transform.position = spwn2.gameObject.transform.position;
        }
        
    }

}
