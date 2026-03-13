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
    }

    void Update()
    {

    }

    public void BuyWeapon()
    {

    }

    void OnTriggerEnter(Collision collision)
    {
        souls = player.GetComponent<player>().souls;
        bladeCost = player.GetComponent<WeaponType>().bladeCost;
        vacuumCost = player.GetComponent<WeaponType>().vacuumCost;
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
