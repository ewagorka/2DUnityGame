using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] PlayerController player;
    public bool isActive = false;
    bool isDead;
    bool nextLevel;
    
    /// <summary>
    /// Handles Pause Menu
    /// </summary>
    private void Update()
    {
        nextLevel = player.nextLevel;
        isDead = player.isDead;


        if (Input.GetKeyDown(KeyCode.Escape))   //Change isActive value every time user clicks Escape
        {
            isActive = !isActive;
        }

        if (isActive)
        {
            if (!isDead && !nextLevel)      //Activate only when no other scrrens are active
            {
                ActivateMenu();
            }
        }
        else
        {if (!isDead && !nextLevel)
            {
                DisactivateMenu();
            }
        }
    }

    /// <summary>
    /// Activates Pause Menu
    /// </summary>
    private void ActivateMenu()
    {
       
            isActive = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;    //Stop the game
        
        
    }

    /// <summary>
    /// Disactivates Pause Menu
    /// </summary>
    public void DisactivateMenu()
    {
        Time.timeScale = 1;         // Play the game
        pauseMenu.SetActive(false);
        isActive = false;           
    }

    /// <summary>
    /// Loads Main Menu scene
    /// </summary>
    public void PlayMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
