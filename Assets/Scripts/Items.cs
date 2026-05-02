using UnityEngine;

public class Items : MonoBehaviour
{
    
    [Header("Item Settings")]
    public bool destroyOnPickup = true;

    public virtual void Interact(player player)
    {
        // used for any potential item, what is here depends on the item collected.
    }

    private void OnTriggerEnter(Collider other)
    {
        player p = other.GetComponent<player>();
        if (p != null)
        {
            Interact(p);
            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
