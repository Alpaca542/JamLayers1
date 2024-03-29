using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpforce = 5f;
    public Rigidbody2D rb;

    public bool IsGrounded = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            IsGrounded = true;
        }
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

        //if (Input.GetKey(KeyCode.Space) && IsGrounded)
        //{
        //    IsGrounded = false;
        //    rb.AddForce(Vector2.up * jumpforce);
        //}
    }
}
