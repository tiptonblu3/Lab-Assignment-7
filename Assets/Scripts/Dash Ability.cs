using UnityEngine;

[CreateAssetMenu(fileName = "DashAbility", menuName = "Abilities/Dash")]
public class DashAbility : Ability
{
    
    public float dashLength = 7f;
    public int soulUpgradeCost = 100;
    
    public override void Activate (GameObject parent)
    {
        if (!canUse()) return; // if can use is false or not

        parent.transform.position += parent.transform.forward * dashLength; // makes player dash in direction they are facing. Could be changed for player input.
        Stats.souls -= soulCost; // takes away souls
    }

    public override void Upgrade()
    {
        // Find the player object and get the player component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player playerSouls = player.GetComponent<player>();

        if (playerSouls.souls < soulUpgradeCost) 
        {
            Debug.LogWarning("Not enough souls to upgrade Dash!");
            Debug.Log("Current Souls: " + playerSouls.souls + ", Required: " + soulUpgradeCost);
            return;
        }
        else 
        {
            playerSouls.souls -= soulUpgradeCost; // Deduct souls for the upgrade
            dashLength += 1f; // Increase dash
            Debug.Log("Dash upgraded! New dash length: " + dashLength);
            soulUpgradeCost += 100; // Increase Soul cost for next upgrade
        }
  
    }
    public override void ResetAbility()
    {
        dashLength = 7f;
        soulUpgradeCost = 100;
    }
    
}
