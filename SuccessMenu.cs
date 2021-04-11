using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SuccessMenu : MonoBehaviour
{

    [SerializeField] GameObject successMenu;
    [SerializeField] PlayerController player;
    public bool nextLevel;

/// <summary>
/// Handles Success Menu
/// </summary>
    private void Update()
    {
        nextLevel = player.nextLevel;       //check if player reached Nect Level trigger
        if (nextLevel)                      // if so Activate screen
        {
            ActivateMenu();
        }

    }

    /// <summary>
    /// Activates Success Menu
    /// </summary>
    private void ActivateMenu()
    {
        Time.timeScale = 0f;        //Stop the game
        successMenu.SetActive(true);
    }

    /// <summary>
    /// Loads Main Menu scene
    /// </summary>
    public void PlayMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
