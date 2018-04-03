using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prot : MonoBehaviour
{

    //public int playerSpeed = 10; //how fast the player moves     
    //public int playerJumpPower = 1250; //how high player jumps
    //private float moveX; //movement on the X plane
    
    public float accSpeed = 18f;
    public float tempSpeed;
    public float distanceToBottomOfPlayer = 0.9f;
    //public GameObject fireball;
    public float speedForce = 20f;
    public Vector2 jumpVector;
    public bool isGrounded;

    public Transform grounder;
    public float radius;
    public LayerMask ground;
    
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //tempSpeed = playerSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speedForce, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speedForce, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(grounder.transform.position, radius, ground);

        if (Input.GetButtonDown("Jump") && isGrounded ==true)
        {
            rb.AddForce(jumpVector, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedForce = (int)accSpeed;
        }
        else
            speedForce = 10f;
        //PlayerMove();
        //PlayerRaycast();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(grounder.transform.position, radius);
    }
    //void PlayerMove()
    //{
    //Controls
    //Animation
    //if(moveX != 0)
    //{
    //GetComponent<Animator>().SetBool("IsRunning", true);
    //}
    //else
    //{
    //GetComponent<Animator>().SetBool("IsRunning", false);
    //}

    //Player Direction
    //if (moveX < 0.0f)
    //{
    //GetComponent<SpriteRenderer>().flipX = true;
    //}
    //else if (moveX > 0.0f)
    //{
    //GetComponent<SpriteRenderer>().flipX = false;
    //}
    //Physics
    //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

    //}

    void Jump()
    {
    //Jumping Code
    //GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    //isGrounded = false;
    }
    //void PlayerRaycast()
    //{
        //TODO fix this ugly code, not working after fixing fireball direction and player move on 4/2/18
        //Ray Up
        //RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        //if (rayUp != false && rayUp.collider != null && rayUp.distance < distanceToBottomOfPlayer && rayUp.collider.name == "Box_2")
        //{
           //Destroy(rayUp.collider.gameObject);
        //}
            
        //Ray Down
        //RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        //if (rayDown != false && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag == "enemy")
        //{
            //GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
            //rayDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
            //rayDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            //rayDown.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            //rayDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //rayDown.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
            //Destroy(hit.collider.gameObject);
        //}
        //if (rayDown != false && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag != "enemy")
        //{
            //isGrounded = true;
        //}
    //}
}