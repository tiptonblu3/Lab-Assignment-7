using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public int soulCost;
    public player Stats;

    public virtual bool canUse() // bool to check whether the skill can be used.
    {
        return Stats.souls >= soulCost;
    }
    
    public abstract void Activate(GameObject parent); // nothing in the base class, inherited objects will have the actual details.
    public abstract void Upgrade(); // similar to Activate

    public abstract void ResetAbility(); // For new game

    
    
   
}
