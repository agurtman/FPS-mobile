using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject car;

    float timer = 0;
    [SerializeField] float cooldown = 3;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            timer = 0;
            GameObject buffer = Instantiate(car, transform);
            buffer.transform.position = transform.position;
        }
    }
}
