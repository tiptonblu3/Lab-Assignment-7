using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public player playerSouls;
    public TextMeshProUGUI scoreText;


    void Awake()
    {
        player playerSouls = GetComponent<player>();
    }

    void Update() 
    {
        scoreText.text = "Souls:" + playerSouls.souls.ToString();
    }
    
}
