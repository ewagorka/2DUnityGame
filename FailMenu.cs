using UnityEngine;
using UnityEngine.SceneManagement;
public class FailMenu : MonoBehaviour
{

    [SerializeField] GameObject failMenu;
    [SerializeField] PlayerController player;
    bool isDead;
    public bool isActive = false;
    public PauseMenu pauseMenu;
    public SuccessMenu successMenu;
    
    /// <summary>
    /// Handles Fail Menu
    /// </summary>
    private void Update()
    {
        isDead = player.isDead;             //check if player is dead

        if (isDead && !pauseMenu.isActive)  //if player is dead, check if Pause Menu isn't active
        {
            ActivateMenu();

        }
        else
        {
            DisactivateMenu();
        }
    }

    /// <summary>
    /// Activates Fail Menu 
    /// </summary>
    private void ActivateMenu()
    {
        if (!pauseMenu.isActive)        // activate only when Pause Menu is not active
        {
            isActive = true;
            failMenu.SetActive(true);
            Time.timeScale = 0f;        //Stop the game
        }

    }
    /// <summary>
    /// Disactivates Fail Menu
    /// </summary>
    public void DisactivateMenu()
    {
        if (!pauseMenu.isActive && !isDead && !successMenu.nextLevel) 
        {
            isActive = false;
            failMenu.SetActive(false);
            Time.timeScale = 1f;            //Play the game
        }
    }

    /// <summary>
    /// Loads Main Menu Scene
    /// </summary>
    public void PlayMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
