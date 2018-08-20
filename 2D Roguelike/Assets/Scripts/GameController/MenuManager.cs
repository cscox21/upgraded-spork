
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        int scene = SceneManager.GetActiveScene().buildIndex +1;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("WE ARE QUITTING THE GAME");
        Application.Quit();
    }

}

