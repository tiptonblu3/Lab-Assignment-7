using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashDistanceCostUI : MonoBehaviour
{
    public DashAbility Dash; // Drag the object with PlayerData here
    public TextMeshProUGUI scoreText;


    void Update() 
    {
        scoreText.text = Dash.soulUpgradeCost.ToString();
    }


}
