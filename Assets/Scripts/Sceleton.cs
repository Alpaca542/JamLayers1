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
    public AnimationClip[] clip;

    [Header("Walking")]
    public GameObject start;
    public GameObject finish;
    public bool ToFinish = true;

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
        float MaxVelocity = Mathf.Max(Mathf.Abs(agent.velocity.x), Mathf.Abs(agent.velocity.y));
        if(MaxVelocity == Mathf.Abs(agent.velocity.x))
        {
            if (agent.velocity.x > 0.2)
            {
                anim.SetFloat("Horizontal", 1);
                if (transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0)) && transform.rotation != Quaternion.Euler(new Vector3(0, 360, 0)) && transform.rotation != Quaternion.Euler(new Vector3(0, -360, 0)))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
            else if (agent.velocity.x < -0.2)
            {
                anim.SetFloat("Horizontal", 1);
                if(transform.rotation != Quaternion.Euler(new Vector3(0, -180, 0)) && transform.rotation != Quaternion.Euler(new Vector3(0, 180, 0)))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }
        }
        else if(MaxVelocity == Mathf.Abs(agent.velocity.y))
        {
            if (agent.velocity.y > 0.2)
            {
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 1);
            }
            else if (agent.velocity.y < -0.2)
            {
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", -1);
            }
        }
    }
    public void KillPlayer()
    {
        System.Random rnd = new System.Random();
        anim.Play(clip[rnd.Next(0, clip.Length)].name);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().die();
    }
}
