using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public Transform[] spots;
    public float speed;
    public Transform[] holes;
    public GameObject projectile;
    public float fireballSpeed;
    public bool vulnerable;
    Vector3 playerPos;

    GameObject player;

	void Start () {
        StartCoroutine("Boss");
        player = GameObject.FindGameObjectWithTag("Player");   
	}

	void Update () {
    }

    IEnumerator Boss()
    {
        while (true)
        {
            // first attack
            while (transform.position.x != spots[0].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, transform.position.y), speed);
                yield return null;
            }

            transform.localScale = new Vector2(-1, 1);
            yield return new WaitForSeconds(.5f);

            int i = 0;
            //while i is smaller than 6, shoot projectiles
            while (i < 6)
            {
                GameObject bossFireBall = (GameObject)Instantiate(projectile, holes[0].position, Quaternion.identity);
                bossFireBall.GetComponent<Rigidbody2D>().velocity = Vector2.left * fireballSpeed;

                //while i is equal to 6, stop shooting projectiles
                i++;
                yield return new WaitForSeconds(1f);
            }

            //second attack
            GetComponent<Rigidbody2D>().isKinematic = true;
            while (transform.position != spots[1].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed);
                yield return null;
            }

            playerPos = player.transform.position;

            yield return new WaitForSeconds(1f);
            GetComponent<Rigidbody2D>().isKinematic = false;

            while (transform.position.x != playerPos.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPos.x, transform.position.y), speed);
                yield return null;
            }
            //we need a deadly tag
            //tag = "Untagged";
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            vulnerable = true;
            yield return new WaitForSeconds(4);
            //tag = "Deadly";
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            vulnerable = false;

            //third attack
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
