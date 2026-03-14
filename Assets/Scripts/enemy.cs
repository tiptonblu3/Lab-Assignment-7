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

    public NavMeshAgent EnemyMonster;
    public Transform Player;
    public player player;
    public WeaponType weapons;
    public Collider collider;
    public GameObject Enemy;


    [Header("Attack Stuff")]
    public float attackSpeed;

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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(EnemyMonster.isOnNavMesh);
        EnemyMonster.stoppingDistance = 0f;
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        Player = playerObj.transform;
        player = playerObj.GetComponent<player>(); // adding these two lines ensures clones of enemy object can hit the player as well

        
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

    private void OnTriggerEnter(Collider collider)
    {   
        
        if(collider.CompareTag("Player")) // Checks for whether object is player
        {
            player p = collider.GetComponent<player>(); // this allows us to set it so that player can take enemy attack in consideration when getting attacked
            Debug.Log("Enemy hit player!");
            p.takeDamage(attack); // player gets damaged when making contact with the enemy's collider box.
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
            Destroy(this.gameObject); //Enemy is dead, we have no use for it, so we destroy the game object.

        }

    }
}
