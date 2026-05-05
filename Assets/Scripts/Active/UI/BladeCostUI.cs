using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BladeCostUI : MonoBehaviour
{
    public BladeShot Blade; // Drag the object with PlayerData here
    public TextMeshProUGUI scoreText;


    void Update() 
    {
        scoreText.text = Blade.soulUpgradeCost.ToString();
    }

}
