using UnityEngine;

[CreateAssetMenu(fileName = "DashAbility", menuName = "Abilities/Dash")]
public class DashAbility : Ability
{
    
    public float dashLength = 7f;
    public int soulUpgradeCost = 100;
    
    public override void Activate (GameObject parent)
    {
        if (!canUse()) return; // if can use is false or not
        
        Vector3 dashDir = parent.transform.forward;
        Vector3 finalPosition;

        // 1. Raycast to see if a wall is in the way
        // We start the ray slightly up (Vector3.up * 0.5f) so it doesn't hit the floor
        if (Physics.Raycast(parent.transform.position + Vector3.up * 0.5f, dashDir, out RaycastHit hit, dashLength))
        {
            // 2. If we hit a wall, stop 0.6 units before the collision point
            // This prevents the player from getting stuck inside the wall's collider
            finalPosition = hit.point - (dashDir * 0.6f);
            
            // Keep the player's original Y position so they don't snap to the floor or ceiling
            finalPosition.y = parent.transform.position.y;
        }
        else
        {
            // 3. No wall? Dash the full length
            finalPosition = parent.transform.position + (dashDir * dashLength);
        }

        parent.transform.position = finalPosition;
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
