using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VaccumCostUI : MonoBehaviour
{
    public VaccumAbility Vaccum; // Drag the object with PlayerData here
    public TextMeshProUGUI scoreText;


    void Update() 
    {
        scoreText.text = Vaccum.soulUpgradeCost.ToString();
    }
}
