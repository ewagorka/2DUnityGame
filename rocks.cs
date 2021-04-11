using UnityEngine;

public class rocks : MonoBehaviour
{

    TimeMachine timeMachine;
    Rigidbody2D rb;
    public bool isDead;
    bool isRewiding;
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeMachine = GetComponent<TimeMachine>();
        startPosition = transform.position;
        
    }


    void Update()
    {
     
        isRewiding = timeMachine.isRewinding;       //check if player is rewinding
        if (isRewiding)
        {
            if (startPosition.y -0.5 <transform.position.y)    //when rock comes back to it's start position area
            {
                rb.gravityScale = 0f;                       // change gravity to 0, so the rock stays in place
                rb.velocity = new Vector2(0, 0);            // set velocity to 0 so previous velocity won't push the rock down

            }
        }

    }

    /// <summary>
    /// Sets rock's gravity to 1 when player enters it's trigger area.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isRewiding && other.gameObject.name.Equals("Player"))  //only when player isn't rewinding
        {
            rb.gravityScale = 1f;                                   // change object's gravity to 0, so it can fall
        }
        

    }

}
