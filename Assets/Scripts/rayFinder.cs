using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class rayFinder : MonoBehaviour
{
    [Header("Objects")]
    public GameObject player;
    public GameObject sceleton;

    [Header("Debug")]
    public bool IsInSight = false;
    public bool IsClose = false;
    public bool NotFound = false;
    public RaycastHit2D[] ListWithoutNulls;
    public RaycastHit2D[] ListWithoutNulls2;

    [Header("Parameters")]
    public float HowFastLoose = 3f;
    public float RayLength = 7f;
    public LayerMask WhatToHit;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            IsInSight = true;
            IsClose = false;
            NotFound = false;
            sceleton.GetComponent<Sceleton>().SawAPlayer(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsInSight = false;
            IsClose = true;
            NotFound = false;
            Invoke(nameof(LostPlayer), HowFastLoose);
        }
    }
    public void LostPlayer()
    {
        IsInSight = false;
        IsClose = false;
        NotFound = true;
    }
    private void Update()
    {
        float maxDistance = 5f;

        // Raycast 1
        Vector2 direction1 = new Vector2(-2.3374f, -5.1216f).normalized;
        Debug.DrawRay(transform.position, direction1 * maxDistance, Color.red);

        // Raycast 2
        Vector2 direction2 = new Vector2(-1.1687f, -5.1216f).normalized;
        Debug.DrawRay(transform.position, direction2 * maxDistance, Color.green);

        // Raycast 3
        Vector2 direction3 = new Vector2(0f, -5.1216f).normalized;
        Debug.DrawRay(transform.position, direction3 * maxDistance, Color.blue);

        // Raycast 4
        Vector2 direction4 = new Vector2(1.1687f, -5.1216f).normalized;
        Debug.DrawRay(transform.position, direction4 * maxDistance, Color.yellow);

        // Raycast 5
        Vector2 direction5 = new Vector2(2.3374f, -5.1216f).normalized;
        Debug.DrawRay(transform.position, direction5 * maxDistance, Color.magenta);

        //Send the rays
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.TransformPoint(new Vector2(-2.3374f, -5.1216f)), RayLength, WhatToHit);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -transform.TransformPoint(new Vector2(-1.1687f, -5.1216f)), RayLength, WhatToHit);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, -transform.TransformPoint(new Vector2(0, -5.1216f)), RayLength, WhatToHit);
        RaycastHit2D hit4 = Physics2D.Raycast(transform.position, -transform.TransformPoint(new Vector2(1.1687f, -5.1216f)), RayLength, WhatToHit);
        RaycastHit2D hit5 = Physics2D.Raycast(transform.position, -transform.TransformPoint(new Vector2(2.3374f, -5.1216f)), RayLength, WhatToHit);
        RaycastHit2D[] raylist = { hit1, hit2, hit3, hit4, hit5 };
        //Check the ray
        bool DoWeSee1 = false;
        foreach (RaycastHit2D hit in raylist)
        {
            if(hit.collider != null)
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    DoWeSee1 = true;
                }
            }
        }
        if(DoWeSee1)
        {
            Debug.Log("We hit");
            IsInSight = true;
            IsClose = false;
            NotFound = false;
            ListWithoutNulls = raylist.Where(n => n.collider != null).ToArray();
            ListWithoutNulls2 = ListWithoutNulls.Where(n => n.collider.gameObject.tag == "Player").ToArray();
            sceleton.GetComponent<Sceleton>().SawAPlayer(ListWithoutNulls[0].collider.gameObject);
        }
        //Color indicators
        if (IsInSight)
        {
            GetComponent<Light2D>().color = new Color32(255, 0, 0, 61); //red
        }
        else if (IsClose)
        {
            GetComponent<Light2D>().color = new Color32(255, 255, 0, 61); //yellow
        }
        else if (NotFound)
        {
            GetComponent<Light2D>().color = new Color32(255, 255, 255, 61); //white
        }
    }
}