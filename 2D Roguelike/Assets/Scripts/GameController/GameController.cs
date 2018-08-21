using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gc;

    public float spawnDelay = 2;
    public string startMusic;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject victoryUI;

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
        
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }
        audioManager.PlaySound(startMusic);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            NextLevel();
        }
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
    }

    public IEnumerator RespawnPlayer()
    {
        
        //Debug.Log("TODO: Death music or sound goes here
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

    public void NextLevel()
    {
        victoryUI.SetActive(true);
    }

}
