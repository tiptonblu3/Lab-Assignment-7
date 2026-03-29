using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class player : MonoBehaviour
{
    [Header("Basic Stats")]
    public float health = 5;
    //public float attack;
    public float defense;
    public float speed = 5;
    public float ultimate = 0;
    public float rotationSpeed = 320f; // Rotation speed in degrees per second
    public float exp = 0;

    [Header("Attack Stuff")]
    public float attackSpeed;
    public float IFrames;

    [Header("Movement Settings")]
    public Vector2 movementInput;
    public Rigidbody rb;
    private Vector3 moveDirection;

    [Header("Shop Stuff")]
    public int souls;


    [Header("GameObjects")]
    public Transform Enemy;
    public gameManager Manager;
    // public enemy EnemyRival;


    [Header("UI References")]
    public TextMeshProUGUI healthText; // 2. Create the reference

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

        if (health <= 0) // prevents health from going below zero.
        {
            health = 0;
            Manager.gameOver();
        }

    }
   
    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString("F0");
        }
    }
}



