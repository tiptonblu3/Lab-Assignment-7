using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    
    public Image bar;
    public GameObject LevelIndic;

    public player player;
    public float xpMax;
    public float currentLevel;
    public bool Levelup = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null)
        {
            xpMax = player.expToNextLevel;
            currentLevel = player.level;
            LevelIndic.SetActive(false);
        }
    }

    void UpdateandUI()
    {
        if (xpMax > 0)
        {
            
            float fill = player.exp / xpMax;

            
            if (fill >= 1f)
            {
                bar.fillAmount = 1f; // Freeze at full
                Levelup = true;      // Turn on bool
            }
            else
            {
                bar.fillAmount = fill;
            }
        }
        else
        {
            bar.fillAmount = 0;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (player == null || bar == null) return;
        
        if (player.level > currentLevel)
        {
            Levelup = true;
        }
        
        if (Levelup)
        {
             bar.fillAmount = 1f; // Freeze at full
             LevelIndic.SetActive(true);
        }
        
        else
        {
            UpdateandUI();
            LevelIndic.SetActive(false);
        }

       
    }

    public void ClearLevelUpIndicator()
    {
        currentLevel = player.level;
        xpMax = player.expToNextLevel;
        Levelup = false;
        LevelIndic.SetActive(false);
    }

}
