using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gc;

    void Start()
    {
        if(gc == null)
        {
            gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

    public IEnumerator RespawnPlayer()
    {
        Debug.Log("TODO: Add waiting for spawn sound");
        yield return new WaitForSeconds(spawnDelay);
        RespawnPlayer();
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        //FindPlayer();
        Debug.Log("TODO: Add Spawn Particles");
    }

    public static void KillPlayer(Player player)
    {
        Debug.Log("Kill player, need to respawn now");
        Destroy(player.gameObject);
        gc.StartCoroutine(gc.RespawnPlayer());
    }
    /*
    public void RespawnPlayer(Player player)
    {
        player.enabled = true;
        player.playerCurrentHealth = player.playerMaxHealth;
        
    }
    */
    /*void FindPlayer()
    {
        GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
        if (searchResult != null)
        playerPrefab = searchResult.transform;
        
    }    
    */
}
