using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieIn3Seconds : MonoBehaviour
{
    public NavMeshPlus.Components.NavMeshSurface baker;
    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        baker = GameObject.FindGameObjectWithTag("bkr").GetComponent<NavMeshPlus.Components.NavMeshSurface>();
        Invoke(nameof(die), 2.5f);
    }
    public void die()
    {
        aud.Play();
        Invoke(nameof(FinalDie), 0.5f);
    }
    public void FinalDie()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        baker.BuildNavMesh();
        Destroy(gameObject);
    }
}
