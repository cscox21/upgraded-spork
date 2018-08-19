using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gc;
    //public Transform playerPrefab;
    //public Transform spawnPoint;
    public float spawnDelay = 2;

    void Awake()
    {
        if(gc == null)
        {
            gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        }
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
    }

    public IEnumerator RespawnPlayer()
    {
        //Debug.Log("TODO: Add waiting for spawn sound");
        yield return new WaitForSeconds(spawnDelay);
        gc.EndGame();
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        //Debug.Log("TODO: Add Spawn Particles");
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gc.StartCoroutine(gc.RespawnPlayer());
    }

}
