using UnityEngine;


public class DashAbility : Ability
{
    // add for later: abilities rn have the same cooldown, change that in later versions. 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float dashLength = 7f;
    
    public override void Activate ()
    {
        if (!canUse()) return; // if can use is false or not

        transform.position += transform.forward * dashLength; // makes player dash in direction they are facing. Could be changed for player input.

        startCooldown();
    }

    public override void Upgrade()
    {
        dashLength += 1f;
        skillCooldown -= 0.2f;
    }
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
