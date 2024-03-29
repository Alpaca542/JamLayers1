using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayFinder : MonoBehaviour
{
    [Header("Objects")]
    public GameObject player;
    public GameObject sceleton;

    [Header("Debug")]
    public bool IsInSight = false;
    public bool IsClose = false;
    public bool NotFound = false;

    [Header("Parameters")]
    public float HowFastLoose = 3f;

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
        //Look at the player
        //transform.right = player.transform.position - transform.position;

        //color indicators
        if (IsInSight)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 61); //red
        }
        else if (IsClose)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 0, 61); //yellow
        }
        else if (NotFound)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 61); //white
        }
    }
}