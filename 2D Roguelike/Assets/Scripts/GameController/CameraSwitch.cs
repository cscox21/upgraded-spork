using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject mainLevelCam;
    public GameObject bossCam;

    AudioListener mainCamAL;
    AudioListener bossCamAL;

    TravelNewArea tna;

    void Awake()
    {
        tna = GetComponent<TravelNewArea>();
    }

    private void Start()
    {
        mainCamAL = mainLevelCam.GetComponent<AudioListener>();
        bossCamAL = mainLevelCam.GetComponent<AudioListener>();
        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
        //switchCamera();
    }

    private void Update()
    {
        /*
        if (canSwitchCam == true)
        {

            switchCamera();
        }
        */
    }


    public void switchCamera()
    {
        cameraChangeCounter();

    }

    void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    void cameraPositionChange(int camPosition)
    {
        if(camPosition >1)
        {
            camPosition = 0;
        }

        PlayerPrefs.SetInt("CameraPosition", camPosition);

        if(camPosition ==0)
        {
            mainLevelCam.SetActive(true);
            mainCamAL.enabled = true;
            bossCamAL.enabled = false;
            bossCam.SetActive(false);
        }

        if (camPosition == 1)
        {
            bossCam.SetActive(true);
            bossCamAL.enabled = true;
            mainCamAL.enabled = false;
            mainLevelCam.SetActive(false);
            
        }

    }
    /*
    public void ShowMainLevel()
    {
        mainLevelCam.enabled = true;
        bossCam.enabled = false;
        

    }

    public void ShowBossLevel()
    {
        bossCam.enabled = true;
        mainLevelCam.enabled = false;
        

    }
    */
}
