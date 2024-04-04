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
        baker = GameObject.FindGameObjectWithTag("bkr").GetComponent<NavMeshPlus.Components.NavMeshSurface>();
        Invoke(nameof(die), 3f);
    }
    public void die()
    {
        aud.PlayOneShot(aud.clip);
        GetComponent<BoxCollider2D>().enabled = false;
        baker.BuildNavMesh();
        Destroy(gameObject);
    }
}
