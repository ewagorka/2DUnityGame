using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] PlayerController player;
    
    /// <summary>
    /// Loads level 1
    /// </summary>
    public void PlayLevel1()
    {
        SceneManager.LoadScene(1);
        player.nextLevel = false;
    }
    /// <summary>
    /// Loads level 2
    /// </summary>
    public void PlayLevel2()
    {
        SceneManager.LoadScene(2);
        player.nextLevel = false;
    }
    /// <summary>
    /// Loads level 1
    /// </summary>
    public void PlayLevel3()
    {
        SceneManager.LoadScene(3);
        player.nextLevel = false; 
    }
}
