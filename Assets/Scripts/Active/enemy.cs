using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class enemy : MonoBehaviour
{
    
    
    [Header("Basic Stats")]
    public float health;
    public float attack;
    public float defense;
    public float speed;
    public float enemyCooldown = 2f;
    private float nextAttackTime;

    public NavMeshAgent EnemyMonster;
    public Transform Player;
    public player player;
    public WeaponType weapons;
    public Collider collider;
    public GameObject Enemy;
    
    

    [Header("Attack Stuff")]
    public float attackSpeed;

    [Header("EXP Drop")]
    public int expMin = 1;
    public int expMax = 5;
    public GameObject expPrefab;

    public GameObject healthUpgradePrefab;
    public float healthDropChance = 0.08f;

    [Header("Enemy Scaling")]
    public float healthperminute = 2f;
    public float attackperminute = 0.2f;


    private bool inVacuumAOEState = false;
    private float originalSpeed;

    // Notes:
    /*
    1. Make parent enemy object (probably call it enemy spawner), and have all enemies spawn under it.
    2. Spawner loop (might have to save that for game manager)
    3. function for player taking damage, might have to do that in player
    */

    public float returnEnemyAttack ()
    {
        return attack;
    }

    public void ScaleEnemy()
    {
        float minutes = Time.timeSinceLevelLoad / 60f;

        health += healthperminute * minutes;
        attack += attackperminute * minutes;

        EnemyMonster.speed = speed; // Ensure speed is set to the base speed (can be modified later for different enemy types)
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(EnemyMonster.isOnNavMesh);
        EnemyMonster.stoppingDistance = 0f;
        originalSpeed = EnemyMonster.speed; // Store original speed for later restoration
        Debug.Log("Enemy original speed: " + originalSpeed); // Debug log to verify original speed is stored correctly
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        Player = playerObj.transform;
        player = playerObj.GetComponent<player>(); // adding these two lines ensures clones of enemy object can hit the player as well

        ScaleEnemy(); // placing it here causes enemies to scale every couple of minutes instead of it checking every frame in update
    }

    // Update is called once per frame
    void Update()
    {


        if (Player != null && EnemyMonster != null)
        {
            EnemyMonster.SetDestination(Player.position); // immediately makes the enemy target the player and march towards them.
            
            /*
            Enemy movement is based on games such as Megabonk and Vampire survivors, in which enemies are constantly targeting the player.
            */
        } 
    }

    private void OnTriggerStay(Collider collider)
    {   
        
        if(collider.CompareTag("Player")) // Checks for whether object is player
        {
            if (inVacuumAOEState)
            {
                // In VacuumAOE state, enemy dies on contact with player
                SpawnEXP();
                TryDropHealth();
                Destroy(this.gameObject);
            }
            else if (Time.time >= nextAttackTime) // checks for whether enemy can attack again based on cooldown
            {
                player p = collider.GetComponent<player>(); // this allows us to set it so that player can take enemy attack in consideration when getting attacked
                Debug.Log("Enemy hit player!");
                p.takeDamage(attack); // player gets damaged when making contact with the enemy's collider box.
                nextAttackTime = Time.time + enemyCooldown; // resets cooldown timer
            }
        }
    }

    public void BladeDamage(float BladeAttack)
    {
        // Basically the same thing as player but for the enemy script instead
    
        float damage = Mathf.Max(BladeAttack - defense, 0); // defense is taken into account
        health -= damage; // health is subtracted based on enemy attack stats
        
        

        if (health <= 0) // prevents health from going below zero.
        {
            health = 0;
            SpawnEXP();
            TryDropHealth();
            Destroy(this.gameObject); //Enemy is dead, we have no use for it, so we destroy the game object.

        }

    }

    public void VacuumDamage(float VacuumAttack) // next objective: Make attacks cooldown based
    {
        // Basically the same thing as player but for the enemy script instead
        
        float damage = Mathf.Max(VacuumAttack - defense, 0); // defense is taken into account
        health -= damage; // health is subtracted based on enemy attack stats

        if (health <= 0) // prevents health from going below zero.
        {
            health = 0;
            EnemyMonster.enabled = false;
            SpawnEXP();
            TryDropHealth();
            Destroy(this.gameObject); //Enemy is dead, we have no use for it, so we destroy the game object.
            
        }

    }

    private void SpawnEXP()
    {
        if (expPrefab == null) // Check if EXP Prefab is REAL!!!
        {
            Debug.LogWarning("EXP prefab not assigned!");
            return;
        }

        int expCount = Random.Range(expMin, expMax + 1); // Spawns a random value between min max
        for (int i = 0; i < expCount; i++)
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * 1f; // Spawns EXP in an area around the dead enemy
            spawnPos.y = transform.position.y; // Keep EXP on the same height as the enemy
            Instantiate(expPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void TryDropHealth() // gives enemies chance to drop health upgrades on death
    {
        if (healthUpgradePrefab == null)
        {
            return;   
        }

        if (Random.value <= healthDropChance)
    {
        Instantiate(healthUpgradePrefab, transform.position, Quaternion.identity);
    }

    }
        
    public void VacuumAOEState()
    {
        // When the player uses this attack the enemy will be dragged towards the player and die upon contact with the player.
        // This attack will have a cooldown, and the player will be invulnerable during the attack.
        // Set enemy NavMeshAgent speed to 5, and make it so that the enemy is dragged towards the player. If the enemy makes contact with the player, the enemy dies.
        inVacuumAOEState = true;
        EnemyMonster.speed = 5f; // Increase speed to drag enemy towards player quickly
        Debug.Log("Enemy entered VacuumAOE state, speed set to 5f. Original speed was: " + originalSpeed);
    }

    public void ExitVacuumAOEState()
    {
        // Exit the vacuum AOE state and restore original speed
        inVacuumAOEState = false;
        EnemyMonster.speed = originalSpeed;
        
        // Push enemy back 5 units away from player
        if (Player != null)
        {
            Vector3 pushDirection = (transform.position - Player.position).normalized;
            transform.position += pushDirection * 5f;
            Debug.Log("Enemy pushed back 5 units.");
        }
        
        Debug.Log("Enemy exited VacuumAOE state, speed restored to: " + originalSpeed);
    }
}
