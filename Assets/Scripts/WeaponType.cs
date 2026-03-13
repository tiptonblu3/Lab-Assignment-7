using UnityEngine;

public class WeaponType : MonoBehaviour
{
    [Header("Basic Stats")]
    public float damage;
    public float cooldown;
    public float projectileSpeed;
    public float area;

    [Header("Settings")]
    public int bladeTier;
    public int vacuumTier;
    public int bladeCost;
    public int vacuumCost;

    

    void Start()
    {
        InitializeWeapon();
    }

    public void InitializeWeapon()
    {
        #region === Blades ===
        // Using the 'tier' variable set in the Inspector
        switch (bladeTier)
        {
            case 1: // Tier 1 +2 Blades
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                bladeCost = 1;
                break;

            case 2: // Tier 2 +2 Blades
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                bladeCost = 1;
                break;

            case 3: // Tier 3 +2 Blades
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                bladeCost = 1;
                break;

            case 4: // Tier 4 +2 Blades
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                bladeCost = 1;
                break;

            default:
                Debug.LogWarning("Tier " + bladeTier + " not defined! Using base stats.");
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                break;
        }
        #endregion === Blades ===

        #region === Vacuum ===
        // Using the 'tier' variable set in the Inspector
        switch (vacuumTier)
        {
            case 1: // Tier 1
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                vacuumCost = 1;
                break;

            case 2: // Tier 2
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                vacuumCost = 1;
                break;

            case 3: // Tier 3
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                vacuumCost = 1;
                break;

            case 4: // Tier 4
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                vacuumCost = 1;
                break;

            default:
                Debug.LogWarning("Tier " + vacuumTier + " not defined! Using base stats.");
                damage = 1;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                break;
        }
        #endregion === Vacuum ===
    }
}