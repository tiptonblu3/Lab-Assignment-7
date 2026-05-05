using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Settings : MonoBehaviour
{
    [Header("UI References")]
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public bool inGame;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;


    [Header("Audio Settings")]
    public AudioMixer mainMixer; // Drag your mixer here in the Inspector

    public void Start()
    {
        // Load the saved volume, or default to 0.75f if no save exists
        float SavedMaster = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        float SavedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float SavedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // Update the UI Sliders to match the saved values
        MasterSlider.value = SavedMaster;
        MusicSlider.value = SavedMusic;
        SFXSlider.value = SavedSFX;

        // Apply the values to the mixer
        ApplyVolume("MusicVolume", SavedMusic);
        ApplyVolume("MusicVolume", SavedMusic);
        ApplyVolume("SFXVolume", SavedSFX);
    }

    public void SetMasterVolume(float volume)
    {
        ApplyVolume("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        ApplyVolume("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        ApplyVolume("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // Helper method to handle the math and mixer application
    private void ApplyVolume(string parameterName, float volume)
    {
        float dB = Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20;
        mainMixer.SetFloat(parameterName, dB);
    }

    public void OpenSettings()
    {
        if(inGame == true)
      {
        pauseMenu.SetActive(false);
      } 

        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
      settingsMenu.SetActive(false); 
      
      if(inGame == true)
      {
        pauseMenu.SetActive(true);
      }  

    } 

    // Optional: Force a save to disk (Unity does this on Quit automatically)
    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}