using UnityEngine;

public class VacuumType : WeaponType
{



    public void InitializeWeapon() // These need a cap or they will break
    {

        #region === Vacuum ===
        // Using the 'tier' variable set in the Inspector
        switch (vacuumTier)
        {
            case 1: // Tier 1 Base
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

    public float returnVacuumAttack()
    {
        return damage;
    }
    private void OnTriggerEnter(Collider vacuumCollider) 
    {   
        
        if(vacuumCollider.CompareTag("Enemy")) // Checks for whether object is player
        {
                enemy e = vacuumCollider.GetComponent<enemy>(); // this allows us to set it so that player can take enemy attack in consideration when getting attacked
                Debug.Log("Enemy hit player!");
                
                if (e != null)
                {
                    e.VacuumDamage(damage); // player gets damaged when making contact with the enemy's collider box.
                }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
