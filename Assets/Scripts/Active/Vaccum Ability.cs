using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "VacuumAOE", menuName = "Abilities/VacuumAOE")]
public class VaccumAbility : Ability
{
     //VacuumAOE ability where enemies are pulled in and take damage over time for 5 seconds. Cooldown of 20 seconds.
    
    public int VaccumAOEDurration = 5;
    public float cooldownTime = 20f;
    public int soulUpgradeCost = 100;


    [System.NonSerialized] 
    private float lastUsedTime = -100f;

    public override bool canUse()
    {
        bool hasSouls = Stats.souls >= soulCost;
        bool isOffCooldown = Time.time >= lastUsedTime + cooldownTime;
        return hasSouls && isOffCooldown;
    }
    public override void Activate(GameObject parent)
    {
            if (!canUse())
        {
            Debug.Log("Not enough souls to use VacuumAOE!");
            return;
        } 

        lastUsedTime = Time.time;
        Stats.souls -= soulCost;

        parent.GetComponent<MonoBehaviour>().StartCoroutine(VacuumRoutine(parent));

    }

    private IEnumerator VacuumRoutine(GameObject parent)
    {
        GameObject[] enemiesObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in enemiesObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                enemy e = obj.GetComponent<enemy>();
                if (e != null)
                {
                    e.VacuumAOEState();
                }
            }
        }

        yield return new WaitForSeconds(VaccumAOEDurration); //wait durration of VacuumAOE

        enemy[] allEnemiesEnd = GameObject.FindObjectsByType<enemy>(FindObjectsSortMode.None);
        foreach (enemy e in allEnemiesEnd)
        {
            e.ExitVacuumAOEState();
        }
        // VacuumAOE ended
    }

    public override void Upgrade()
    {
        // Find the player object and get the player component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player playerSouls = player.GetComponent<player>();

        if (playerSouls.souls < soulUpgradeCost) 
        {
            Debug.LogWarning("Not enough souls to upgrade VacuumAOE!");
            Debug.Log("Current Souls: " + playerSouls.souls + ", Required: " + soulUpgradeCost);
            return;
        }
        else 
        {
            playerSouls.souls -= soulUpgradeCost; // Deduct souls for the upgrade
            VaccumAOEDurration += 1; // Increase duration
            Debug.Log("VacuumAOE upgraded! New duration: " + VaccumAOEDurration);
            soulUpgradeCost += 100; // Increase Soul cost for next upgrade
        }

    }
    public override void ResetAbility()
    {
        VaccumAOEDurration = 5;
        cooldownTime = 20f;
        soulUpgradeCost = 100;
    }
}
