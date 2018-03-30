using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prot : MonoBehaviour
{

    public int playerSpeed = 10; //how fast the player moves     
    public int playerJumpPower = 1250; //how high player jumps
    private float moveX; //movement on the X plane
    public bool isGrounded;
    public float accSpeed = 15f;
    public float tempSpeed;
    public float distanceToBottomOfPlayer = 0.9f;
    public GameObject fireball;

    void Start()
    {
        tempSpeed = playerSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRaycast();
    }
    void PlayerMove()
    {
        //Controls
        moveX = Input.GetAxis("Horizontal");
        //if you press the jump button AND you are grouned, you can jump
        if(Input.GetButtonDown("Jump") && isGrounded == true) 
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = (int)accSpeed;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = (int)tempSpeed;
        }

        //Animation
        if(moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
        }

        //Player Direction
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //Physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        //Range attack
        //if(Input.GetButtonDown("Fire1"))
        //{
            //GameObject newFireball = Instantiate(fireball, transform.position, transform.rotation);
        //}
    }

    void Jump()
    {
        //Jumping Code
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
    }
    void PlayerRaycast()
    {
        //TODO fix this ugly code
        //Ray Up
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUp != false && rayUp.collider != null && rayUp.distance < distanceToBottomOfPlayer && rayUp.collider.name == "Box_2")
        {
            Destroy(rayUp.collider.gameObject);
        }
            
        //Ray Down
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if (rayDown != false && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag == "enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayDown.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
            //Destroy(hit.collider.gameObject);
        }
        if (rayDown != false && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag != "enemy")
        {
            isGrounded = true;
        }
    }
}