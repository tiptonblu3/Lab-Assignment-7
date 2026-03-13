using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public int souls;
    public int bladeCost;
    public int vacuumCost;
    public gameobject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        souls = player.GetComponent<player>().souls;
        bladeCost = player.GetComponent<WeaponType>().bladeCost;
        vacuumCost = player.GetComponent<WeaponType>().vacuumCost;
    }

    void Update()
    {

    }

    public void BuyWeapon()
    {

    }


    public void BuyVacuumUpgrade()
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
