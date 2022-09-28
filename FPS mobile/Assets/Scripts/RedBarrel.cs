using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBarrel : MonoBehaviour
{
    [SerializeField] float radius = 3;
    [SerializeField] GameObject boomParticle;

    public void Boom()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;

        var enemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, enemies[i].transform.position) < radius)
            {
                enemies[i].Dead();
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            player.GetComponent<PlayerController>().ChangeHealth(-80);
        }
        GameObject boomVFX = Instantiate(boomParticle);
        boomVFX.transform.position = transform.position;
        GetComponent<AudioSource>().Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 1);
    }
}
