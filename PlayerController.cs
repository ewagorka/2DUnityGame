using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Player Body/Animation components
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //Colliders
    BoxCollider2D boxCollider2D;


    //Parameters
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private bool isGrounded;
    private bool isJumping;
    private bool isOnLadder = false;
    public bool isDead = false;
    public bool nextLevel = false;

    //Movement variables
    public float horizontalMove;
    public float verticalMove;

    //Layers
    [SerializeField] private LayerMask groundLayer;

    //Time Machine
    TimeMachine timeMachine;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        timeMachine = GetComponent<TimeMachine>();

    }

    /// <summary>
    ///Calls GroundCheck, HandleAnimation and Move functions.
    /// </summary>
    // Update is called once per frame
    void Update()
    {

        Move();                                             // Move the player

    }

    private void FixedUpdate()
    {
        GroundCheck();                                      // Check if player is on the ground
        HandleAnimation();                                  // Change animations
    }

    /// <summary>
    /// Handles players movement by getting vertical and horizontal ipnuts.
    /// </summary>
    private void Move()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        if (timeMachine.isRewinding == false)
        {
            if (!isDead)
            {

                //----MOVEMENT---

                // HORIZONTAL
                if (horizontalMove > 0)
                {
                    rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y); // when horizontal input is greater than 0 move player to the right with appropriate speed
                    spriteRenderer.flipX = false;                                      // when moving to the right don't flip the sprite
                }
                if (horizontalMove < 0)
                {

                    rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y); // when horizontal input is lower than 0 move player to the right with appropriate speed
                    spriteRenderer.flipX = true;                                       // when moving to the right flip the sprite
                }

                //JUMPING
                if (Input.GetKey(KeyCode.Space) && isGrounded)      // jump only when player is grounded and when Space key is pressed
                {
                    isJumping = true;
                    rb.velocity = new Vector2(horizontalMove, jumpForce);   //jump with appropriate force

                }

                //----LADDERS----

                if (isOnLadder) //when player is on ladder
                {
                    rb.velocity = new Vector2(rb.velocity.x / 2, verticalMove * speed / 2); // move the player with half of the regular speed
                    rb.gravityScale = 0f;                                                   //change gravity to 0
                }
                else
                {
                    rb.gravityScale = 1.5f;     //when the player is off the ladder bring gravity back to .5f
                }
            }
        }
        else    //when the player is rewinding
        {
            if (isDead && timeMachine.time > 1)   //Check if the player is dead and if they still have rewinding time
            {
                isDead = false;                 //reset player's dead state back to false
            }
        }
    }

    /// <summary>
    /// Checks if the player is grounded, by comparing the position of the player's box collider and 
    /// ground layer.
    /// </summary>
    void GroundCheck()
    {
        if (boxCollider2D.IsTouchingLayers(groundLayer)) // if box collider and ground layer are touching
        {
            isGrounded = true;                           // player is grounded  
            isJumping = false;                           // player landed
        }
        else if (!boxCollider2D.IsTouchingLayers(groundLayer))
        {
            isGrounded = false;
        }

    }

    /// <summary>
    /// Function checks if player collided with either rocks or spikes by comparing player their colliders' positions.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(("Rock")) || collision.gameObject.CompareTag(("Spikes"))) // if player contacted object with "Rock" 
        {                                                                                            // or "Spikes" tag, change player's dead state to true
            isDead = true;
        }
    }

    /// <summary>
    /// Checks if the player is on the ladder or reached the end of the level, by comparing trigger areas' tags and player's position.
    /// If the player is in the trigger area it changes it's state, or loads "nextLevel" screen.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Ladder"))) //when player enters "Ladder" trigger area
        {
            isOnLadder = true;                              //player is on a ladder         
        }
        if (collision.gameObject.CompareTag(("NextLevel"))) // when player enters "NextLevel" trigger area
        {
            nextLevel = true;                               // load the "nextLevel" screen
        }
    }

    /// <summary>
    /// Checks if player is off the ladder.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Ladder"))) //when player exits "Ladder" trigger area
        {
            isOnLadder = false;                         // player is no longer on a ladder
        }
    }

    /// <summary>
    /// Changes animations, by setting different parameters in the Animator.
    /// </summary>
    void HandleAnimation()
    {

        //Run and Idle
        animator.SetFloat("HSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VSpeed", Mathf.Abs(verticalMove));
        animator.SetBool("Ground", isGrounded);

        //Jump
        animator.SetBool("Jump", isJumping);

        //Death
        animator.SetBool("Dead", isDead);

        //Time rewinding
        animator.SetBool("IsRewinding", timeMachine.isRewinding);

        //Ladders
        animator.SetBool("Climb", isOnLadder);
        if (isOnLadder)
        {

            if (verticalMove == 0 && this.animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Climb"))  //Stop animation on ladder when not moving
            {
                animator.speed = 0f;
            }
            else
            {
                animator.speed = 1;
            }
        }
        else
        {
            animator.speed = 1;     //allow other animations to play with regular speed
        }
    }
}
