using UnityEngine;

public class WeaponType : MonoBehaviour
{
    // all elements in this script are shared across the Blade and Vacuum type scripts, so they were left here for organization purposes.
    
    
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

    [Header("Weapon Hitboxes")]
    public Collider bladeCollider;
    public Collider vacuumCollider;


    

    void Start()
    {
        
    }
    

}