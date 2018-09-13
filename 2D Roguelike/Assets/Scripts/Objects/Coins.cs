using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

    public int pointsToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>() == null)
        {
            return; 
        }
        
        ScoreManager.AddPoints(pointsToAdd);
        
        Destroy(gameObject);
    }
}
