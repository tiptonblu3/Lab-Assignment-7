using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public bool IsPaused;
    public GameObject PauseMenu;

    // Using the new Input System, we can set up an action for pausing the game.
    public InputAction pauseAction;

    public void OnPause()
    {
        IsPaused = !IsPaused; // Toggle the pause state

        if (IsPaused)
        {
            Time.timeScale = 0f; // Pause the game
            // Optionally, you can also show a pause menu here
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            // Optionally, hide the pause menu here
        }
        
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(IsPaused); // Show or hide the pause menu based on the pause state
        }
    }


}
