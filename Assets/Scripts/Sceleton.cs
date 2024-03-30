using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus;

public class Sceleton : MonoBehaviour
{
    [Header("System")]
    public NavMeshAgent agent;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject rayFinder;
    public GameObject player;

    [Header("Debug")]
    public bool AmIChasing;

    [Header("Walknig")]
    public GameObject start;
    public GameObject finish;
    public bool ToFinish = true;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    public void SawAPlayer(GameObject Target)
    {
        AmIChasing = true;
        CancelInvoke(nameof(StopChasing));
        Invoke(nameof(StopChasing), 3f);
    }
    void StopChasing()
    {
        AmIChasing = false;
    }
    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we touch finsh && ToFinish
        if((collision.gameObject == finish && ToFinish) || (collision.gameObject == start && !ToFinish))
        {
            ToFinish = !ToFinish;
        }
    }
    private void Update()
    {
        //Chase if the player is nearby
        if (AmIChasing)
        {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
        else
        {
            if (ToFinish)
            {
                agent.SetDestination(finish.transform.position);
            }
            else
            {
                agent.SetDestination(start.transform.position);
            }

        }


        //Rotate to the player's side
        if (agent.velocity.x > 0.2 && transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0)))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if(agent.velocity.x < -0.2 && transform.rotation != Quaternion.Euler(new Vector3(0, 0, -180)) && transform.rotation != Quaternion.Euler(new Vector3(0, 0, 180)))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
}
