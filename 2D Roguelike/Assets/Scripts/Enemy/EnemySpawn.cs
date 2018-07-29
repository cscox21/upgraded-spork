using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject enemy;
    public Transform enemyPos;
    float spawnRate = 3.0f;

	// Use this for initialization
	void Start ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvokeRepeating("EnemySpawner", 0.5f, spawnRate);
        Destroy(gameObject, 11);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void EnemySpawner()
    {
        Instantiate(enemy, enemyPos.position, enemyPos.rotation);
    }
}
