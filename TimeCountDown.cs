using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCountDown : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public TimeMachine timeMachine;
    public float secondsLeft;
    int timeLeft;
    // Start is called before the first frame update
    private void Start()
    {
        timeMachine = GetComponent<TimeMachine>();
    }
    
    /// <summary>
    /// Takes available rewind time from TimeMachine and displays it on the screen.
    /// </summary>
    void FixedUpdate()
    {
        secondsLeft = timeMachine.time;
        timeLeft = (int)secondsLeft;
        timeDisplay.text = "Available rewind time: " + timeLeft;
    }


}
