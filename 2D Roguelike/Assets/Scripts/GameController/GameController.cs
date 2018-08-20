using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gc;

    public float spawnDelay = 2;
    public string spawnSoundName;


    [SerializeField]
    private GameObject gameOverUI;

    //cache
    private AudioManager audioManager;

    void Awake()
    {
        if(gc == null)
        {
            gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        }
    }

    private void Start()
    {
        //caching
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
    }

    public IEnumerator RespawnPlayer()
    {
        audioManager.PlaySound(spawnSoundName);
        //Debug.Log("TODO: Add waiting for spawn sound");
        yield return new WaitForSeconds(spawnDelay);
        gc.EndGame();
        //int scene = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(scene, LoadSceneMode.Single);
        //Debug.Log("TODO: Add Spawn Particles");
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gc.StartCoroutine(gc.RespawnPlayer());
    }

}
