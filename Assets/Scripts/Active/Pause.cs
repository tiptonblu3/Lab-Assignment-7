using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public bool IsPaused;
    public GameObject PauseMenu;
    public Settings settings;
    // Using the new Input System, we can set up an action for pausing the game.
    public InputAction pauseAction;

    public void OnPause()
    {
        IsPaused = !IsPaused; // Toggle the pause state

        if (IsPaused)
        {
            Time.timeScale = 0f; // Pause the game
            settings.SetPauseMute(true);
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            settings.SetPauseMute(false);
        }
        
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(IsPaused); // Show or hide the pause menu based on the pause state
        }
    }


}
