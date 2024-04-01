using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckKostil : MonoBehaviour
{
    void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        if (!Physics2D.OverlapCircle(transform.position, 0.05f))
        {
            Destroy(gameObject);
        }
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
