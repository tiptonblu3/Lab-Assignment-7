using UnityEngine;

public class BladeType : WeaponType
{

    public float rotationSpeed = 50f;
    
    public void InitializeWeapon() // These need a cap or they will break
    {
        #region === Blades ===
        // Using the 'tier' variable set in the Inspector
        switch (bladeTier)
        {
            case 1: // Tier 1 +2 Blades Base
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
                //Debug.LogWarning("Tier " + bladeTier + " not defined! Using base stats.");
                damage = 3;
                cooldown = 1;
                projectileSpeed = 1;
                area = 1;
                break;
        }
        #endregion === Blades ===

        
    }

    public float returnBladeAttack()
    {
        return damage;
    }
    
    
    private void OnTriggerEnter(Collider BladeCollider) 
    {   
        if(BladeCollider.CompareTag("Enemy")) // Checks for whether object is player
        {

                enemy e = BladeCollider.GetComponent<enemy>(); // this allows us to set it so that player can take enemy attack in consideration when getting attacked
                if (e != null)
                {
                    player p = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
                    e.BladeDamage(damage + p.bonusAttack); // player gets damaged when making contact with the enemy's collider box.
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
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
    }
}
