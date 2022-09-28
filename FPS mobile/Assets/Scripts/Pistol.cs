using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    [SerializeField] protected GameObject rifleStart;
    [SerializeField] protected GameObject shootParticle;
    [SerializeField] protected Text ammoText;
    [SerializeField] protected Camera cam;
    [SerializeField] protected GameObject impact;
    [SerializeField] protected GameObject pistolUI;
    [SerializeField] protected GameObject rifleUI;
    [SerializeField] protected GameObject shotgunUI;
    protected int ammo;
    protected int ammoAll;
    protected int ammoMax;
    private float shootTimer = 0;
    protected float shootCooldown;
    protected float range;
    protected int damage;

    protected virtual void Start()
    {
        ammoAll = 100;
        ammoMax = 10;
        shootCooldown = 0.3f;
        range = 100;
        damage = 5;
        Reload();
    }

    protected virtual void Update()
    {
        pistolUI.SetActive(true);
        rifleUI.SetActive(false);
        shotgunUI.SetActive(false);
    }

    public void Shoot()
    {
        if (ammo > 0 && this.transform.parent != null)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > shootCooldown)
            {
                shootTimer = 0;
                GameObject shootVFX = Instantiate(shootParticle);
                shootVFX.transform.position = rifleStart.transform.position;
                Destroy(shootVFX, 0.2f);
                ammo -= 1;
                ammoText.text = ammo + "/" + ammoAll;

                RaycastHit shootObj;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out shootObj, range))
                {
                    Debug.Log(shootObj.collider.name);
                    GameObject inst = Instantiate(impact, shootObj.point, Quaternion.LookRotation(shootObj.normal));
                    Destroy(inst, 0.5f);

                    if (shootObj.rigidbody != null)
                    {
                        shootObj.rigidbody.AddForce(-shootObj.normal * 100);
                    }

                    if (shootObj.collider.tag == "Enemy")
                    {
                        shootObj.collider.GetComponent<Enemy>().ChangeHealth(-damage);
                    }
                    if (shootObj.collider.tag == "RedBarrel")
                    {
                        shootObj.collider.GetComponent<RedBarrel>().Boom();
                    }
                }
            }
        }
    }

    public void AddAmmo(int count)
    {
        ammoAll += count;
        ammoText.text = ammo + "/" + ammoAll;
    }

    public void Reload()
    {
        if (ammo < ammoMax)
        {
            int ammoNeed = ammoMax - ammo;
            if (ammoAll >= ammoNeed)
            {
                ammoAll -= ammoNeed;
                ammo += ammoNeed;
            }
            else
            {
                ammo += ammoAll;
                ammoAll = 0;
            }
            ammoText.text = ammo + "/" + ammoAll;

            if (ammoAll != 0)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
