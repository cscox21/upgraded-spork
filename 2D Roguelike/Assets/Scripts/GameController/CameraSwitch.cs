using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public Camera mainLevelCam;
    public Camera bossCam;

    public void ShowMainLevel()
    {
        bossCam.enabled = false;
        mainLevelCam.enabled = true;
        
    }

    public void ShowBossLevel()
    {
        mainLevelCam.enabled = false;
        bossCam.enabled = true;
        
    }
}
