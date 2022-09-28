public class Rifle : Pistol
{
    protected override void Start()
    {
        ammoAll = 300;
        ammoMax = 30;
        shootCooldown = 0.1f;
        range = 100;
        damage = 10;
        Reload();
    }

    protected override void Update()
    {
        pistolUI.SetActive(false);
        rifleUI.SetActive(true);
        shotgunUI.SetActive(false);
    }
}