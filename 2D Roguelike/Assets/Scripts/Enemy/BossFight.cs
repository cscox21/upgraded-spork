using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour {

    public Transform[] spots;
    public Transform[] fireLocation;
    public float speed;
    public GameObject projectile;
    public GameObject trackingProjectile;
    public float trackerSpeed;
    public float fireballSpeed;
    GameObject player;
    Vector3 playerPos;

    



	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Boss");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator Boss()
    {

        while (true)
        {
            //First attack
            //While the boss's x position isnt at the 1st spot, move 
            while (transform.position.x != spots[0].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, transform.position.y), speed);
                yield return null;
            }

            //flip the boss on the x axis
            transform.localScale = new Vector2(-1, 1);
            yield return new WaitForSeconds(1f);

            int i = 0;
            while (i < 3)
            {
                GameObject bossFireball = (GameObject)Instantiate(projectile, fireLocation[0].position, Quaternion.identity);
                bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.left * fireballSpeed;

                i++;
                yield return new WaitForSeconds(1f);
            }

            //Second Attack
            GetComponent<Rigidbody2D>().isKinematic = true;
            while (transform.position != spots[2].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[2].position, speed);
                i = 0;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            while (i < 3)
            {
                GameObject bossFireball = (GameObject)Instantiate(trackingProjectile, fireLocation[1].position, Quaternion.identity);
                bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.up * trackerSpeed;

                i++;
                yield return new WaitForSeconds(1f);
            }




            playerPos = player.transform.position;

            yield return new WaitForSeconds(2.5f);
            GetComponent<Rigidbody2D>().isKinematic = false;

            while (transform.position.x != playerPos.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPos.x, transform.position.y), speed);
                yield return null;
            }

            //Third Attack
            Transform temp;
            if (transform.position.x > player.transform.position.x)

                temp = spots[2];
            else
                temp = spots[0];

            while (transform.position.x != temp.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(temp.position.x, transform.position.y), speed);
                if (temp.position.x > transform.position.x)
                {
                    //face right
                    transform.localScale = new Vector2(-1, 1);
                }
                else if (temp.position.x < transform.position.x)
                {
                    //face left
                    transform.localScale = new Vector2(1, 1);
                }

                yield return null;
            }

        }

    }
}
