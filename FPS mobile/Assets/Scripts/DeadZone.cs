using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] float waitCooldown;
    [SerializeField] float moveCooldown;
    [SerializeField] float sizeSpeed;
    [SerializeField] GameObject player;
    float waitTime;
    float moveTime;
    float timer;
    float cooldown = 1;
    bool inZone;

    void Update()
    {
        waitTime += Time.deltaTime;

        if (waitTime > waitCooldown)
        {
            moveTime += Time.deltaTime;

            transform.localScale -= new Vector3(sizeSpeed, 0, sizeSpeed);

            if (moveTime > moveCooldown)
            {
                waitTime = 0;
                moveTime = 0;
            }
        }

        timer += Time.deltaTime;
        if (timer > cooldown && inZone == false)
        {
            timer = 0;
            player.GetComponent<PlayerController>().ChangeHealth(-2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = false;
        }
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = true;
        }
    }
}
