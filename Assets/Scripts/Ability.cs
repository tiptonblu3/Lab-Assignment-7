using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    // ability ideas
    /*
        1. Dash
        2. Super Shield / AOE attack
        3. Long strike
    */

    public float skillCooldown = 5f; // the cooldown of the skill
    public float lastUsed; // when the skill was last used.

    public bool canUse () // bool to check whether the skill can be used.
    {
        return Time.time >= skillCooldown - lastUsed;
    }

    public void startCooldown () // marks when skill enters cooldown
    {
        lastUsed = Time.time;
    }
    
    public abstract void Activate(); // nothing in the base class, inherited objects will have the actual details.
    public abstract void Upgrade(); // similar to Activate
    

}
