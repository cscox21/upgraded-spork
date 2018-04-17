using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Move_Prot : MonoBehaviour
{

    //public int playerSpeed = 10; //how fast the player moves     
    //public int playerJumpPower = 1250; //how high player jumps
    //public float moveX; //movement on the X plane
    
    public float accSpeed = 18f;
    public float tempSpeed;
    public float distanceToBottomOfPlayer = 0.9f;
    //public GameObject fireball;
    public float speedForce = 20f;
    public Vector2 jumpVector;
    public bool isGrounded;

    private Animator anim;
    private bool playerMoving;
    private Vector2 lastMove;

    public Transform grounder;
    public float radius;
    public LayerMask ground;
    
    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       
        //tempSpeed = playerSpeed;
    }
    // Update is called once per frame
    void Update()
    {

        playerMoving = false;

        if(Input.GetAxisRaw("Horizontal")> 0.5f || Input.GetAxisRaw("Horizontal") <-0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speedForce * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);

        //if (Input.GetKey(KeyCode.D))
        //{
        //rb.velocity = new Vector3(speedForce, rb.velocity.y);
        //transform.localScale = new Vector3(1, 1, 1);

        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //rb.velocity = new Vector3(-speedForce, rb.velocity.y);
        //transform.localScale = new Vector3(-1, 1, 1);

        //}
        //else
        // rb.velocity = new Vector2(0, rb.velocity.y);
        // GetComponent<Animator>().SetBool("IsRunning", false); 

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

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(grounder.transform.position, radius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Deadly")
        {
            Debug.Log("Dead");
            SceneManager.LoadScene("Level_01");
        }
    }
    
    
    
    //Animation


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
    void PlayerRaycast()
    {
        //TODO fix this ugly code
        //Ray Up
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUp != false && rayUp.collider != null && rayUp.distance < distanceToBottomOfPlayer && rayUp.collider.tag == "Block")
        {
           Destroy(rayUp.collider.gameObject);
        }
            
        //Ray Down
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if (rayDown != false && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayDown.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
            Destroy(rayDown.collider.gameObject);
        }
        if (rayDown != false && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag != "Enemy")
        {
            isGrounded = true;
        }
    }
}