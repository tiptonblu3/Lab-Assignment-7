using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Basic Stats")]
    public float damage;
    public float cooldown;
    public float projectileSpeed;
    public float area;

    [Header("Type Bools")]
    bool isRanged;
    bool isMelee;
    // two types of weapons, melee and ranged. Weapons inherit from this class and have their own unique behaviors. 
    // bools are temporary, maybe could be used for unique interactions with enemies, but for now, stays as its own thing.




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
