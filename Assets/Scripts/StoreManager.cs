using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public int souls;
    public int bladeCost;
    public int vacuumCost;
    public GameObject player;
    private void InitializeWeapon()
    {
        player.GetComponent<BladeType>().InitializeWeapon();
        player.GetComponent<VacuumType>().InitializeWeapon();
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

    void OnTriggerEnter(Collider collision)
    {
        souls = player.GetComponent<player>().souls; // Updates Player Souls
        bladeCost = player.GetComponent<WeaponType>().bladeCost; // Updates Cost
        vacuumCost = player.GetComponent<WeaponType>().vacuumCost; // Updates Cost
        // after a few seconds open store

    }
    void OnTriggerExit(Collider collision)
    {
        InitializeWeapon(); // On leaving the store weapons are updated
    }

    public void BuyVacuumUpgrade() // 
    {
        if (souls >= vacuumCost)
        {
            souls -= vacuumCost;
            player.GetComponent<VacuumType>().vacuumTier++;
        }
    }

    public void BuyBladeUpgrade()
    {
        if (souls >= bladeCost)
        {
            souls -= bladeCost;
            player.GetComponent<BladeType>().bladeTier++;
        }
    }

}
