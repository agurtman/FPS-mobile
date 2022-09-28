using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Text moneyText;
    [SerializeField] GameObject shopUI;
    private List<GameObject> inventoryGuns;
    GameObject pistol;
    GameObject rifle;
    GameObject shotgun;
    public int money;
    bool isBuyPistol;
    bool isBuyRifle;
    bool isBuyShotgun;
    bool isOpen;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            Debug.Log("Load");
            GetMoney(PlayerPrefs.GetInt("Money"));
            moneyText.text = "Money: " + money.ToString();
        }
        else
        {
            Debug.Log("money = 100");
            GetMoney(100);
        }

        inventoryGuns = GetComponent<GunSwitcher>().inventoryGuns;
        pistol = GetComponent<GunSwitcher>().pistol;
        rifle = GetComponent<GunSwitcher>().rifle;
        shotgun = GetComponent<GunSwitcher>().shotgun;
    }

    public void GetMoney(int count)
    {
        money += count;
        moneyText.text = "Money: " + money.ToString();
    }

    public void OpenShop()
    {
        if (!isOpen)
        {
            shopUI.SetActive(true);
            GetComponent<PlayerLook>().enabled = false;
            isOpen = true;
        }
        else
        {
            shopUI.SetActive(false);
            GetComponent<PlayerLook>().enabled = true;
            isOpen = false;
        }
    }

    public void BuyPistol()
    {
        if (money >= 100 && !isBuyPistol)
        {
            isBuyPistol = true;
            GetMoney(-100);

            inventoryGuns.Add(pistol);
        }
    }

    public void BuyRifle()
    {
        if (money >= 500 && !isBuyRifle)
        {
            isBuyRifle = true;
            GetMoney(-500);

            inventoryGuns.Add(rifle);
        }
    }

    public void BuyShotgun()
    {
        if (money >= 300 && !isBuyShotgun)
        {
            isBuyShotgun = true;
            GetMoney(-300);

            inventoryGuns.Add(shotgun);
        }
    }

    public void BuyAmmoPistol()
    {
        if (money >= 50 && inventoryGuns.Contains(pistol))
        {
            GetMoney(-50);
            pistol.GetComponent<Pistol>().AddAmmo(25);
        }
    }

    public void BuyAmmoRifle()
    {
        if (money >= 100 && inventoryGuns.Contains(rifle))
        {
            GetMoney(-100);
            rifle.GetComponent<Rifle>().AddAmmo(50);
        }
    }

    public void BuyAmmoShotgun()
    {
        if (money >= 75 && inventoryGuns.Contains(shotgun))
        {
            GetMoney(-75);
            shotgun.GetComponent<Shotgun>().AddAmmo(25);
        }
    }

    public void Save()
    {
        Debug.Log("Save");
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        Debug.Log("Reset");
        PlayerPrefs.SetInt("Money", 100);
        PlayerPrefs.Save();
    }
}
