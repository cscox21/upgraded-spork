using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour {

    public int EnemySpeed;
    public int XMoveDirection;

	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));

        //moves the enemy
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
        if(hit.distance <0.7f) //if the ray gets within .7f of what it hits, then Flip
        {
            Flip();
            if(hit.collider.tag == "Player")
            {
                Destroy(hit.collider.gameObject);
                SceneManager.LoadScene("Level_01");
            }
        }
	}

    //will flip the enemy on the x direction
    void Flip()
    {
        if(XMoveDirection >0)
        {
            XMoveDirection = -1;
        }
        else
        {
            XMoveDirection = 1;
        }
    }
}

