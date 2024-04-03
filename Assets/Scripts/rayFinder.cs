using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
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

    private void Update()
    {
        //Send the checking rays
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(-2.3374f, 5.1216f)), RayLength, WhatToHit);

        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(-1.1687f, 5.1216f)), RayLength, WhatToHit);

        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(0, 5.1216f)), RayLength, WhatToHit);

        RaycastHit2D hit4 = Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(1.1687f, 5.1216f)), RayLength, WhatToHit);

        RaycastHit2D hit5 = Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(2.3374f, 5.1216f)), RayLength, WhatToHit);

        RaycastHit2D[] raylist = { hit1, hit2, hit3, hit4, hit5 };

        //Send the attack ray
        RaycastHit2D hitAttack = Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(0, 5.1216f)), 1f, WhatToHit);

        //Check the checking rays
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
            IsClose = true;
            NotFound = false;
            ListWithoutNulls = raylist.Where(n => n.collider != null).ToArray();
            ListWithoutNulls2 = ListWithoutNulls.Where(n => n.collider.gameObject.tag == "Player").ToArray();
            sceleton.GetComponent<Sceleton>().SawAPlayer(ListWithoutNulls[0].collider.gameObject);
        }
        else
        {
            IsInSight = false;
            NotFound = true;
        }

        //Chech the attack rays
        if(hitAttack.collider != null)
        {
            if (hitAttack.collider.gameObject.tag == "Player")
            {
               sceleton.GetComponent<Sceleton>().KillPlayer();
            }
            else
            {
                if (hitAttack.collider.gameObject.tag == "Lure")
                {
                    Destroy(hitAttack.collider.gameObject);
                }
            }
        }


        //Color indicators
        if (IsInSight)
        {
            GetComponent<Light2D>().color = new Color32(255, 0, 0, 61); //red
            sceleton.GetComponent<NavMeshAgent>().speed = 6f;
        }
        else if (NotFound || IsClose)
        {
            GetComponent<Light2D>().color = new Color32(255, 255, 255, 61); //white
            sceleton.GetComponent<NavMeshAgent>().speed = 3f;
        }
    }
}