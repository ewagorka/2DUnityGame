using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{

    public bool isRewinding = false;
    public float time;
    public List<Vector2> positions;     //List to store player's positions
     Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;                                   //Set available rewind time to 0
        positions = new List<Vector2>(20);
        rb = GetComponent<Rigidbody2D>();

    }

    /// <summary>
    /// Checks if user presses Enter and if is rewinding
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isRewinding = true;
            
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            isRewinding = false;
            
        }
            
    }
    /// <summary>
    /// Manages whether the rewinding or recording positions should be performed
    /// </summary>
    private void FixedUpdate()
    {
            if (isRewinding)
            {
                Rewind();       //When user is rewinding, play back players positions
            }
            else
            {
                Record();   //When user is not rewinding, record players positions
            }  
    }

    /// <summary>
    /// Rewinds player back in time by changing their current position to the previous one. It only does it when the player have rewinding time available.
    /// </summary>
    void Rewind()
    {
        
        if ((int)time > 0)      
        {
            time -= Time.deltaTime;     // subtract time
            if (positions.Count > 0)    //if there are any positons in the list
            {
                transform.position = positions[0]; // change player's cyrrent position to the previous one
                positions.RemoveAt(0);              // remove position that just has been played
            }
            else              // if the list is empy, don't rewind
            {
                isRewinding = false;
            }
        }
        else                
        {
            isRewinding = false;
            time -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Records players positions from the last 20sec of gameplay.
    /// </summary>
    void Record()
    {
        if (!isRewinding)       //when not rewinding start resetting the time buffer
        {
            if (time <= 20)
            {
                time += Time.deltaTime;
            }
        
        }
        if (positions.Count > Mathf.Round(20f / Time.fixedDeltaTime)) // If the list is full 
        {
            positions.RemoveAt(positions.Count - 1);        //Start removing oldest positions
        }
        positions.Insert(0, transform.position);        // Insert current positions at the beginning of the list
    }

 
}
