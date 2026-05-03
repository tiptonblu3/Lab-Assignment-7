using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public GameObject GameOverScreen;
    public StoreManager StoreMan;
    public player player;
    public void RestartScene()
    {
        GameOverScreen.SetActive(false);
        StoreMan.UIButtons.SetActive(true);
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene("SampleScene"); 
        
    }

}
