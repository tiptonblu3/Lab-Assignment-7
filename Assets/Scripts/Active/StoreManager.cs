using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public int souls;
    public int bladeCost;
    public int vacuumCost;
    public GameObject player;
    public GameObject blade;
    public GameObject vacuum;
    public GameObject Shop;
    public GameObject UIButtons; 
    private float storeOpenTime;
    public bool shopOpened = false;



    private void InitializeWeapon()
    {
        blade.GetComponent<BladeType>().InitializeWeapon();
        vacuum.GetComponent<VacuumType>().InitializeWeapon();
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Finds Player
        blade = GameObject.FindGameObjectWithTag("Blade"); // Finds Blade
        vacuum = GameObject.FindGameObjectWithTag("Vacuum"); // Finds Vacuum
    }

    void Update()
    {
        if (!shopOpened && storeOpenTime > 0 && Time.time >= storeOpenTime + 1.5f)
        {
            Shop.SetActive(true);
            UIButtons.SetActive(false);
            Time.timeScale = 0f; // Pause game
            Debug.Log("Store Opened");
            shopOpened = true;
        }
    }

    public void BuyWeapon()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        souls = player.GetComponent<player>().souls; // Updates Player Souls
        bladeCost = blade.GetComponent<WeaponType>().bladeCost; // Updates Cost
        vacuumCost = vacuum.GetComponent<WeaponType>().vacuumCost; // Updates Cost
        InitializeWeapon(); // Updates weapons
        // Start timer to open store after seconds
        storeOpenTime = Time.time;
        shopOpened = false;
    }
    void OnTriggerExit(Collider collision)
    {
        InitializeWeapon(); // On leaving the store weapons are updated
        Shop.SetActive(false);
        UIButtons.SetActive(true); // Deactivate UI buttons when leaving the store
        shopOpened = false;
        storeOpenTime = 0; // Reset timer so shop can't reopen
        Debug.Log("Store Closed");
    }
    public void CloseShop()
    {
        Shop.SetActive(false);
        UIButtons.SetActive(true);
        shopOpened = false;
        storeOpenTime = 0;
        Debug.Log("Store Closed");
        Time.timeScale = 1f; // Resume game
    }
    public void BuyVacuumUpgrade() // 
    {
        if (souls >= vacuumCost)
        {
            souls -= vacuumCost;
            vacuum.GetComponent<VacuumType>().vacuumTier++;
        }
    }

    public void BuyBladeUpgrade()
    {
        if (souls >= bladeCost)
        {
            souls -= bladeCost;
            blade.GetComponent<BladeType>().bladeTier++;
        }
    }

}
