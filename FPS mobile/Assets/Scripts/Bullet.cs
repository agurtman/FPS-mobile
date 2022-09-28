using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private Vector3 direction;

    private void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.2f);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().ChangeHealth(-1);
        }
        if (other.tag == "RedBarrel")
        {
            other.GetComponent<RedBarrel>().Boom();
        }
    }
}
