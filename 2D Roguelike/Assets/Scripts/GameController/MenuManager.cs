
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

    public void LoadLevelOne()
    {
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadLevelTwo()
    {
        int scene = SceneManager.GetActiveScene().buildIndex + 2;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadLevelThree()
    {
        int scene = SceneManager.GetActiveScene().buildIndex + 3;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadLevelFour()
    {
        int scene = SceneManager.GetActiveScene().buildIndex + 4;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}

