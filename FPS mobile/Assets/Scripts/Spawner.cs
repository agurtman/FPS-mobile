using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject tree;
    [SerializeField] float radius = 10;

    float timer = 0;
    float cooldown = 5;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            timer = 0;
            GameObject buffer = Instantiate(tree, transform);
            float x = Random.Range(-radius, radius);
            float z = Random.Range(-radius, radius);
            buffer.transform.position = transform.position + new Vector3(x, 0, z);
        }
    }
}