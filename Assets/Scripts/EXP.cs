using UnityEngine;

public class EXP : MonoBehaviour
{
    public float speed = 5f;
    public float expAmount = 10f;
    public AudioClip expSound;
    
    private GameObject player;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player by tag
        player = GameObject.FindWithTag("Player");
        
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            // Calculate step size based on speed and time
            float step = speed * Time.deltaTime;
            
            // Move our position a step closer to the target
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
    }

    // Destroy self on collision with player tag
    private void OnTriggerEnter(Collider collider) 
    {   
        if(collider.CompareTag("Player")) // Checks for whether object is player
        {
            // Play sound effect
            if (expSound != null && audioSource != null)
            {
                AudioSource.PlayClipAtPoint(expSound, transform.position);
            }
            
            // Give player EXP
            player playerScript = collider.GetComponent<player>();
            if (playerScript != null)
            {
                playerScript.exp += expAmount;
            }
            
            // Destroy self on collision with player tag
            Destroy(gameObject);
            Debug.Log("Boom EXP");
        }
    }
}
