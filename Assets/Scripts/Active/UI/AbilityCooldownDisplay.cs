using UnityEngine;
using TMPro; 

public class AbilityCooldownDisplay : MonoBehaviour
{
    [Header("References")]
    public VaccumAbility ability; // Drag your VacuumAOE asset here
    public TextMeshProUGUI cooldownText; // Drag your TMP object here

    void Update()
        {
            // 1. Get the raw time left
            float timeLeft = ability.GetSecondsLeft();

            // 2. Update the text
            if (timeLeft > 0)
            {
                // "F0" means 0 decimal places (10, 9, 8...)
                // "F1" would show 1 decimal place (10.5, 10.4...)
                cooldownText.text = timeLeft.ToString("F0");

            }
            else
            {
                // Clear the text when the ability is ready
                cooldownText.text = "READY"; 
                
            }
        }

}
