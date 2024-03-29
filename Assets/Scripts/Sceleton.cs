using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus;

public class Sceleton : MonoBehaviour
{
    public NavMeshAgent agent;

    public Rigidbody2D rb;

    public Animator anim;

    public GameObject rayFinder;

    public GameObject player;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    public void SawAPlayer(GameObject Target)
    {
        agent.SetDestination(Target.transform.position);
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        //Rotate to the player's side
        if (agent.velocity.x > 0.2 && transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0)))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //rayFinder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if(agent.velocity.x < -0.2 && transform.rotation != Quaternion.Euler(new Vector3(0, 0, -180)) && transform.rotation != Quaternion.Euler(new Vector3(0, 0, 180)))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //rayFinder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
    }
}
