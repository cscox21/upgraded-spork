using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZom_Move : MonoBehaviour
{

    public Transform[] patrolpoints;
    int currentPoint;
    public float speed = 0.5f;
    public float timeStill = 1.2f;
    public float sight = 5f;
    Animator anim;

    public GameObject projectile;
    public Transform firePos;

    float nextBasicAttack;
    public float basicAttackRate = .25f;

    public float headHeight;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Patrol");
        //basicAttackRate = .3f;
        nextBasicAttack = Time.time;
        anim.SetBool("Walking", true);

    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (transform.position.x == patrolpoints[currentPoint].position.x)
            {
                currentPoint++;
                anim.SetBool("Walking", false);
                yield return new WaitForSeconds(timeStill);
                anim.SetBool("Walking", true);
            }

            if (currentPoint >= patrolpoints.Length)
            {
                currentPoint = 0;
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolpoints[currentPoint].position.x, transform.position.y), speed);

            Debug.DrawRay(transform.position + Vector3.up * headHeight, transform.right * -sight, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * headHeight, transform.right * sight, Color.blue);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * headHeight, transform.right, sight);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position + Vector3.up * headHeight, transform.right, -sight);

            if (transform.localScale.x == -1)
            {
                if (hit.collider != null && hit.collider.tag == "Player")

                {
                    Shoot();
                }
            }
            if (transform.localScale.x == 1)
            {
                if (rightHit.collider != null && rightHit.collider.tag == "Player")
                //if (rightHit.collider != null && rightHit.collider.tag == "Player")
                {
                    Shoot();
                }
            }


            if (transform.position.x < patrolpoints[currentPoint].position.x)

                transform.localScale = new Vector3(-1, 1, 1);
            else if (transform.position.x > patrolpoints[currentPoint].position.x)
                transform.localScale = Vector3.one;
            //transform.localScale = new Vector3(-1, 1, 1);
            yield return null;
        }
    }

    void Shoot()
    {
        if (Time.time > nextBasicAttack)
        {
            Instantiate(projectile, firePos.position, Quaternion.identity);
            nextBasicAttack = Time.time + basicAttackRate;
        }
    }
}