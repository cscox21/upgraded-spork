using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombiePatrol : MonoBehaviour {

    public Transform[] patrolpoints;
    int currentPoint;
    public float speed = 0.5f;
    public float timeStill = 1.2f;
    public float sight = 3f;

    Animator anim;
    public float force;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Patrol");
        anim.SetBool("Walking", true);

    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
        if (hit.collider != null && hit.collider.tag == "Player")
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position)*force);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            Destroy(other.gameObject);
            SceneManager.LoadScene("Level_01");
        }
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

            if (transform.position.x < patrolpoints[currentPoint].position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (transform.position.x > patrolpoints[currentPoint].position.x)
                transform.localScale = Vector3.one;
            yield return null;

        }


        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * -sight);
    }

}
