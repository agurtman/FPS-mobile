using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    [SerializeField] public List<GameObject> inventoryGuns = new List<GameObject>();
    [SerializeField] private GameObject currentGun;
    [SerializeField] public GameObject pistol;
    [SerializeField] public GameObject shotgun;
    [SerializeField] public GameObject rifle;
    [SerializeField] GameObject weaponUI;
    bool shoot;

    private void Update()
    {
        if (shoot)
        {
            if (currentGun == pistol)
            {
                pistol.GetComponent<Pistol>().Shoot();
            }
            if (currentGun == rifle)
            {
                rifle.GetComponent<Rifle>().Shoot();
            }
            if (currentGun == shotgun)
            {
                shotgun.GetComponent<Shotgun>().Shoot();
            }
        }
    }

    public void Reload()
    {
        if (currentGun == pistol)
        {
            pistol.GetComponent<Pistol>().Reload();
        }
        if (currentGun == rifle)
        {
            rifle.GetComponent<Rifle>().Reload();
        }
        if (currentGun == shotgun)
        {
            shotgun.GetComponent<Shotgun>().Reload();
        }
    }

    public void OnPointerDown()
    {
        shoot = true;
    }

    public void OnPointerUp()
    {
        shoot = false;
    }

    public void ChoosePistol()
    {
        if (inventoryGuns.Contains(pistol))
        {
            for (int i = 0; i < inventoryGuns.Count; i++)
            {
                inventoryGuns[i].SetActive(false);
            }
            currentGun = pistol;
            pistol.SetActive(true);
        }
        weaponUI.SetActive(false);
    }

    public void ChooseRifle()
    {
        if (inventoryGuns.Contains(rifle))
        {
            for (int i = 0; i < inventoryGuns.Count; i++)
            {
                inventoryGuns[i].SetActive(false);
            }
            currentGun = rifle;
            rifle.SetActive(true);
        }
        weaponUI.SetActive(false);
    }

    public void ChooseShotgun()
    {
        if (inventoryGuns.Contains(shotgun))
        {
            for (int i = 0; i < inventoryGuns.Count; i++)
            {
                inventoryGuns[i].SetActive(false);
            }
            currentGun = shotgun;
            shotgun.SetActive(true);
        }
        weaponUI.SetActive(false);
    }

    public void WeaponSwitch()
    {
        weaponUI.SetActive(true);
    }
}