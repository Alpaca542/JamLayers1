using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieIn3Seconds : MonoBehaviour
{
    public NavMeshPlus.Components.NavMeshSurface baker;
    // Start is called before the first frame update
    void Start()
    {
        baker = GameObject.FindGameObjectWithTag("bkr").GetComponent<NavMeshPlus.Components.NavMeshSurface>();
        Invoke(nameof(die), 3f);
    }
    public void die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        baker.BuildNavMesh();
        Destroy(gameObject);
    }
}
