using UnityEngine;

public class VacuumType : WeaponType
{
    public GameObject vacuumProjectile;
    public Transform firePoint;
    public float lastShotTime; // when was vacuum shot last fired. This is used alongside cooldown
    public bool isProjectile = false;
    public float lastVacuumAOETime; // when was vacuumAOE last used. This is used alongside cooldown for that ability
    


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
                //Debug.LogWarning("Tier " + vacuumTier + " not defined! Using base stats.");
                damage = 10;
                cooldown = 1;
                projectileSpeed = 15;
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
            Debug.Log("Player hit enemy!");
                
            if (e != null)
            {
                e.VacuumDamage(damage); // player hits enemy
                    
            }

            Destroy(this.gameObject); // vacuum hit enemy, we don't need game object anymore.
        }
    }

    void shoot()
    {
        Vector3 spawnOffset = transform.forward * 1.5f;
        
        GameObject projectile = Instantiate(vacuumProjectile, transform.position + spawnOffset, transform.rotation);

        
        // behavior for clone instances of bullet
        VacuumType projClone = projectile.GetComponent<VacuumType>();

        if (projClone != null) // sets most variables same as normal bullet.
        {
            projClone.isProjectile = true;
            projClone.projectileSpeed = projectileSpeed;
            projClone.damage = damage;


        }
    }

    void move()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    public void shotCooldown()
    {
        if(isProjectile) return;
        
        if (Time.time >= lastShotTime + cooldown)
        {
            shoot();
            lastShotTime = Time.time;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!isProjectile)
        {
           InitializeWeapon(); 
        }
        else
        {
            Destroy(this.gameObject, 5f); // destroy projectile after 5 seconds
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isProjectile)
        {
            move();
        }
        else
        {
            
        }
    }

        public void VacuumAOE()
    {
        // References the enemy script to call the VacuumAOEState method for 5 seconds and then the ExitVacuumAOEState method
        // then will activate a 20 second cooldown for the vacuumAOE ability
        
        // Check if ability is on cooldown
        if (Time.time < lastVacuumAOETime + 20f)
        {
            Debug.Log("VacuumAOE is on cooldown!");
            return;
        }
        
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
        
        Invoke("ExitVacuumAOE", 5f); // Exit VacuumAOE state after 5 seconds
        lastVacuumAOETime = Time.time; // Start cooldown
        Debug.Log("VacuumAOE activated! Cooldown will end in 20 seconds.");
    }

    void ExitVacuumAOE()
    {
        GameObject[] enemiesObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in enemiesObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                enemy e = obj.GetComponent<enemy>();
                if (e != null)
                {
                    e.ExitVacuumAOEState();
                }
            }
        }
    }
}
