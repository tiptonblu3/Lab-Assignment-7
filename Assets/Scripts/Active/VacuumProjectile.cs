using UnityEngine;

public class VacuumProjectile : MonoBehaviour
{
    [Header("Settings (Used if Setup fails)")]
    public float defaultSpeed = 15f;
    public float defaultDamage = 10f;
    public float spinSpeed = 1000f;

    private float speed;
    private float damage;
    private Vector3 moveDirection;
    private bool isReady = false;
    private Rigidbody rb;

    void Start()
    {
        // If Setup() wasn't called by the ability, use defaults
        if (!isReady)
        {
            Debug.LogWarning("Setup not called by Ability! Using default values.");
            Setup(defaultSpeed, defaultDamage);
        }
    }

    public void Setup(float projectileSpeed, float projectileDamage)
    {
        rb = GetComponent<Rigidbody>();
        speed = projectileSpeed;
        damage = projectileDamage;
        moveDirection = transform.forward;
        isReady = true;
        
        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        if (!isReady || rb == null) return;

        // Move position
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

        // Continuous Rotation
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * spinSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy e = other.GetComponent<enemy>();
            if (e != null) e.VacuumDamage(damage);
            
            Debug.Log($"Hit {other.name} for {damage} damage!");
            Destroy(gameObject); 
        }
    }
}