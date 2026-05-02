using UnityEngine;

public class HealthIncrease : Items
{
    public float HealthIncreaseAmount = 1f;
    
    
    public override void Interact(player player)
    {
        player.Heal(HealthIncreaseAmount);

        Debug.Log("Player HP gained: " + HealthIncreaseAmount);
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
