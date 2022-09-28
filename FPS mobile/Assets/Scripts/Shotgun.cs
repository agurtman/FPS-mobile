public class Shotgun : Pistol
{

    protected override void Start()
    {
        ammoAll = 75;
        ammoMax = 1;
        range = 5;
        damage = 20;
        Reload();
    }

    protected override void Update()
    {
        pistolUI.SetActive(false);
        rifleUI.SetActive(false);
        shotgunUI.SetActive(true);
    }
}
