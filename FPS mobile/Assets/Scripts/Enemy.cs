using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Transform> points = new List<Transform>();
    [SerializeField] [Range(0, 360)] private float ViewAngle = 90f;
    [SerializeField] private float ViewDistance = 15f;     
    [SerializeField] private Transform Target;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject rifleStart;
    public static int enemiesCount;
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] Transform newTarget;
    float timer = 0;
    private int health = 100;
    bool isDead = false;

    void Start()
    {
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetDestination();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);
        if (isDead == false)
        {
            if (IsInView())
            {
                if (distanceToPlayer >= 5f)
                    MoveToTarget();
                else
                {
                    timer += Time.deltaTime;
                    agent.isStopped = true;
                    anim.SetBool("idle", true);
                    transform.LookAt(newTarget.transform);
                    if (timer > 2)
                    {
                        timer = 0;
                        GameObject buffer = Instantiate(bullet);
                        buffer.GetComponent<Bullet>().SetDirection(transform.forward);
                        buffer.transform.position = rifleStart.transform.position;
                        buffer.transform.rotation = transform.rotation;
                        anim.SetTrigger("hit");
                    }
                }
            }

            else
            {
                agent.isStopped = false;           
                if (agent.remainingDistance < .25f)
                {
                    StartCoroutine("Idle");
                }
            }

            //DrawView();
        }

    }

    private bool IsInView()
    {
        float currentAngle = Vector3.Angle(transform.forward, Target.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, ViewDistance))
        {
            if (currentAngle < ViewAngle / 2f && Vector3.Distance(transform.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
            {
                return true;
            }
        }
        return false;
    }

    public void SetDestination()
    {
        Vector3 newTarget = points[Random.Range(0, points.Count)].position;
        agent.SetDestination(newTarget);
    }

    private void MoveToTarget()
    {
        agent.isStopped = false;
        agent.speed = 3.5f;
        agent.SetDestination(Target.position);
    }

    IEnumerator Idle()
    {
        agent.speed = 0;
        SetDestination();
        anim.SetBool("idle", true);
        yield return new WaitForSeconds(5);
        agent.speed = 3.5f;
        anim.SetBool("idle", false);     
    }

    //private void DrawView()
    //{
    //    Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, ViewAngle / 2f, 0)) * (transform.forward * ViewDistance);
    //    Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, ViewAngle / 2f, 0)) * (transform.forward * ViewDistance);
    //    Debug.DrawLine(transform.position, left, Color.yellow);
    //    Debug.DrawLine(transform.position, right, Color.yellow);
    //}

    public void ChangeHealth(int count)
    {
        health += count;
        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        anim.SetTrigger("dead");
        agent.isStopped = true;
        Target.GetComponent<Shop>().GetMoney(100);
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        enemiesCount -= 1;
        isDead = true;

        if (enemiesCount == 0)
        {
            Target.GetComponent<PlayerController>().WinGame();
        }
    }
}
