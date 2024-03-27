using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float MovementX = Input.GetAxis("Horizontal");
        float MovementY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(MovementX * speed, MovementY * speed);
    }
}
