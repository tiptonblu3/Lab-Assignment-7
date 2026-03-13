using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public int souls;
    public int bladeCost;
    public int vacuumCost;
    public gameobject player;
    private void InitializeWeapon()
    {
        player.GetComponent<WeaponType>().InitializeWeapon();
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Finds Player
    }

    void Update()
    {

    }

    public void BuyWeapon()
    {

    }

    void OnTriggerEnter(Collision collision)
    {
        souls = player.GetComponent<player>().souls; // Updates Player Souls
        bladeCost = player.GetComponent<WeaponType>().bladeCost; // Updates Cost
        vacuumCost = player.GetComponent<WeaponType>().vacuumCost; // Updates Cost
        // after a few seconds open store

    }
    void OnTriggerExit(Collision collision)
    {
        InitializeWeapon(); // On leaving the store weapons are updated
    }

    public void BuyVacuumUpgrade() // 
    {
        if (souls >= vacuumCost)
        {
            souls -= vacuumCost;
            player.GetComponent<WeaponType>().vacuumTier++;
        }
    }

    public void BuyBladeUpgrade()
    {
        if (souls >= bladeCost)
        {
            souls -= bladeCost;
            player.GetComponent<WeaponType>().bladeTier++;
        }
    }

}
