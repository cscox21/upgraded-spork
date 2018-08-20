using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("BACK TO MAIN MENU");
        int scene = SceneManager.GetActiveScene().buildIndex -1;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Retry()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

}
