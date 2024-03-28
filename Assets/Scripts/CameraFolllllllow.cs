using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolllllllow : MonoBehaviour
{
    public GameObject hero;
    void Update()
    {
        Vector3 fixedVector = hero.transform.position;
        fixedVector.z -= 10;
        transform.position = Vector3.Lerp(transform.position, fixedVector, Time.deltaTime * 5f);
    }
}
