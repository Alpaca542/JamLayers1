using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Parameters")]
    public float speed = 5f;
    bool AmIDead = false;

    [Header("Debug")]
    public Rigidbody2D rb;
    public Animator anim1;
    public AnimationClip clipDie;
    public DieManager diemng;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float MovementX = Input.GetAxis("Horizontal");
        float MovementY = Input.GetAxis("Vertical");
        if(MovementX < 0 && transform.rotation != Quaternion.Euler(0, 180, 0) && transform.rotation != Quaternion.Euler(0, -180, 0))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (MovementX > 0 && transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rb.velocity = new Vector2(MovementX, MovementY).normalized * speed;
    }
    public void die()
    {
        if (!AmIDead)
        {
            AmIDead = true;
            anim1.SetBool("Death", true);
            diemng.ToTheMenu();
        }
    }
}
