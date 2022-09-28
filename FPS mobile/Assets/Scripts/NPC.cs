using UnityEngine;

public class NPC : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    private float timer = 0;
    private float speed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        anim.SetFloat("move", 0);
        if (timer > 2)
        {
            controller.Move(transform.forward * speed * Time.deltaTime);
            anim.SetFloat("move", 0.5f);
            speed = 3;
        }
        if (timer > 5)
        {
            controller.Move(transform.forward * speed * Time.deltaTime);
            anim.SetFloat("move", 1);
            speed = 5;
        }
        if (timer > 8)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            timer = 0;
        }
    }
}