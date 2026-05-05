using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System.Collections;


public class player : MonoBehaviour
{
    [Header("Basic Stats")]
    public float health = 5;
    public float maxHealth = 5;
    //public float attack;
    public float defense;
    public float speed = 5;
    public float ultimate = 0;
    public float rotationSpeed = 320f; // Rotation speed in degrees per second
    
    [Header("Abilities")]
    public List<Ability> abilities; // Drag your assets here in the Inspector

    public static event Action<float> OnHealthChanged;
    public static event Action OnDeath;

    [Header("Attack Stuff")]
    public float attackSpeed;
    public float IFrames;

    [Header("Movement Settings")]
    public Vector2 movementInput;
    public Rigidbody rb;
    private Vector3 moveDirection;

    [Header("Shop Stuff")]
    public int souls;
    public float exp = 0;

    [Header("Level System")]
    public int level = 1;
    public float expToNextLevel = 100f;
    public float maxHealthPerLevel = 2f;
    public float maxAttackPerLevel = 0.2f;
    public float bonusAttack  = 0f;
    public float passiveExp = 1f; // amount of EXP gained passively per second

    [Header("GameObjects")]
    public Transform Enemy;
    public gameManager Manager;
    // public enemy EnemyRival;
    public SceneReloader screl;

    [Header("UI References")]
    public TextMeshProUGUI healthText; // health text
    public TextMeshProUGUI soulText; // Souls text

    [Header("Materials")]
    public Material NormalMat;
    public Material HurtMat;
    public MeshRenderer PlayerRend;
    private bool isHurt = false;

    private void OnMove(InputValue inputValue)
    {
        //grab values from unity input system (for both mediums) and set it to this variable
        movementInput = inputValue.Get<Vector2>();

        //Debug.Log(inputValue.Get<Vector2>()); to show a button was pressed and recieved for debugging purposes, can be removed later
    }
    private void FixedUpdate()
    {
        ManageMovement();
    }


    private void ManageMovement()
    {
       if (movementInput.magnitude > 0.1f)
    {
        // Calculate the direction in World Space
        Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y).normalized;

        // Apply Velocity
        // We keep the Y velocity so gravity still works
        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);

        // Smooth Rotation
        // LookRotation defines the target. RotateTowards moves us there at 'rotationSpeed'
        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        
        rb.MoveRotation(Quaternion.RotateTowards(
            transform.rotation, 
            targetRotation, 
            rotationSpeed * Time.fixedDeltaTime)
        );
    }
    else
    {
        // Optional: Stop the player from sliding when input is released
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
    }
    }

    public void takeDamage(float enemyAttack) // next objective, make player take damage every frame
    {
        // Health, enemy attack, and defense are taken into consideration
        // When hit, damage is subtracted by player defense, then that total is subtracted from the total health of the player
        // Each enemy shares same attack pattern, with the only difference being how much damage they do.

        float damage = Mathf.Max(enemyAttack - defense, 0); // defense is taken into account

        health -= damage; // health is subtracted based on enemy attack stats
        StartCoroutine(SwapRoutine());
        OnHealthChanged?.Invoke(health); //goes through every method to see what is calling the health value

        if (health <= 0) // prevents health from going below zero.
        {
            OnDeath?.Invoke(); //delegate that invokes when death happens (in game manager)
            Time.timeScale = 0f;
            health = 0;
        }

    }

    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        OnHealthChanged?.Invoke(health);
    }

    public void checkLevelUp()
    {
        while (exp >= expToNextLevel)
        {
            exp -= expToNextLevel;

            level++;

            bonusAttack += maxAttackPerLevel;
            maxHealth += maxHealthPerLevel;
            health = maxHealth; // optional full heal

            maxAttackPerLevel *= 1.5f; // Increase the attack bonus for the next level

            expToNextLevel *= 1.5f;

            OnHealthChanged?.Invoke(health);

            Debug.Log("Level Up! Level " + level);
        }
    }

    public void GainExp(float amount)
    {
        exp += amount;
    }

    void Start()
    {
        
        // Ensure the health isn't 0 from the Inspector
        if (health <= 0) health = 5; 

        // Tell the UI/Delegates what the starting health is
        OnHealthChanged?.Invoke(health);
    }
    // Update is called once per frame
    void Update()
    {
        
       try
        {
            if (healthText != null)
                healthText.text = "Health: " + health.ToString("F0");

            if (soulText != null)
                soulText.text = "Souls: " + souls.ToString("F0");
        }
        catch (System.Exception e)
        {
            Debug.LogError("UI update failed: " + e.Message);
        }

        checkLevelUp();
        exp += passiveExp * Time.deltaTime; // Gain passive EXP over time
    }

    #region  Abilities 
    public void UseAbility(int index)
    {
        // 1. Safety check: Does the slot exist?
        if (index < 0 || index >= abilities.Count)
        {
            Debug.LogWarning($"Ability slot {index} is empty!");
            return;
        }

        Ability selectedAbility = abilities[index];

        if (selectedAbility != null)
        {
            // 2. The Handshake: Tell the scriptable object who the player is
            selectedAbility.Stats = this;

            // 3. Activate the ability logic
            selectedAbility.Activate(gameObject);
        }
    }
        #region Input Callbacks for Abilities
        public void OnAbilityOne(InputValue value)
        {
            if (value.isPressed) UseAbility(0); //do nothing for now for controller/keyboard use
        }

        public void OnAbilityTwo(InputValue value)
        {
            if (value.isPressed) UseAbility(1); //do nothing for now for controller/keyboard use
        }

        public void OnAbilityThree(InputValue value)
        {
            if (value.isPressed) UseAbility(2); //do nothing for now for controller/keyboard use
        }
        #endregion
    public void UpgradeAbility(int index)
    {
        // Check if the slot exists and isn't empty
        if (index >= 0 && index < abilities.Count && abilities[index] != null)
        {
            abilities[index].Upgrade();
        }
        else
        {
            Debug.LogWarning("Upgrade failed: Slot " + index + " is empty.");
        }
    }

    #endregion

    IEnumerator SwapRoutine()
    {
        // If we are already flashing, don't restart the logic or we'll overwrite the 'Normal' save
        if (isHurt) yield break; 

        isHurt = true;

        // 1. Save and Swap
        // Tip: Use sharedMaterial to avoid creating internal copies if you don't need them
        NormalMat = PlayerRend.sharedMaterial; 
        PlayerRend.material = HurtMat;

        // 2. Wait
        yield return new WaitForSeconds(1.0f);

        // 3. Swap back
        PlayerRend.material = NormalMat;
        
        isHurt = false; // Reset the gate
    }



}



