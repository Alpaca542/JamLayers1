using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    public AnimationClip clipHit;
    public LayerMask LureLayer;

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
        rayFinder.GetComponent<rayFinder>().IsClose = false;
        rayFinder.GetComponent<rayFinder>().IsInSight = false;
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
        if(collision.gameObject.tag == "Player")
        {
            rayFinder.GetComponent<rayFinder>().IsInSight = true;
            SawAPlayer(collision.gameObject);
        }
        if(collision.gameObject.tag == "Lure")
        {
            Destroy(collision.gameObject);
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
            if (Physics2D.OverlapCircle(transform.position, 6f, LureLayer))
            {
                agent.SetDestination(GameObject.FindGameObjectWithTag("Lure").transform.position);
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
        }


        //Rotate to the player's side
        float MaxVelocity = Mathf.Max(Mathf.Abs(agent.velocity.x), Mathf.Abs(agent.velocity.y));
        if(MaxVelocity == Mathf.Abs(agent.velocity.x))
        {
            if (agent.velocity.x > 1)
            {
                anim.SetFloat("Horizontal", 1);
                rayFinder.transform.position = transform.TransformPoint(new Vector2(0.3f, 0.15f));
                rayFinder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));

                if (transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0)) && transform.rotation != Quaternion.Euler(new Vector3(0, 360, 0)) && transform.rotation != Quaternion.Euler(new Vector3(0, -360, 0)))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
            else if (agent.velocity.x < -1)
            {
                anim.SetFloat("Horizontal", 1);
                rayFinder.transform.position = transform.TransformPoint(new Vector2(0.3f, 0.15f));
                rayFinder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                if (transform.rotation != Quaternion.Euler(new Vector3(0, -180, 0)) && transform.rotation != Quaternion.Euler(new Vector3(0, 180, 0)))
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }
        }
        else if(MaxVelocity == Mathf.Abs(agent.velocity.y))
        {
            if (agent.velocity.y > 1)
            {
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 1);
                rayFinder.transform.position = transform.TransformPoint(new Vector2(0, 0.5f));
                rayFinder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (agent.velocity.y < -1)
            {
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", -1);
                rayFinder.transform.position = transform.TransformPoint(new Vector2(0, -0.15f));
                rayFinder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
        }
    }
    public void KillPlayer()
    {
        agent.SetDestination(transform.position);
        rayFinder.GetComponent<rayFinder>().enabled = false;
        Camera.main.GetComponent<Camera>().orthographicSize = 2;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        anim.Play(clipHit.name);
        Invoke(nameof(InvokeKill), 0.5f);
    }
    public void InvokeKill()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().die();
    }
}
