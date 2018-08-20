
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scene_02");
    }

    public void QuitGame()
    {
        Debug.Log("WE ARE QUITTING THE GAME");
        Application.Quit();
    }

}

